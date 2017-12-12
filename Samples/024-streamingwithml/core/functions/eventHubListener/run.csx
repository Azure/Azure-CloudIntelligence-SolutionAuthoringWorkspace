#r "Newtonsoft.Json"

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

public static async Task Run(string eventHubMessage, TraceWriter log)
{
    var sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

    var tweet = JsonConvert.DeserializeObject<Tweet>(eventHubMessage);

    await Score(tweet, log);

    using (SqlConnection conn = new SqlConnection(sqlConnectionString))
    {

        conn.Open();
        using (SqlCommand cmd = new SqlCommand(
            @"
                INSERT INTO [dbo].[TweetScore]
                (
                    timestamp,
                    topic,	
                    score,
                    sentiment,
                    text,
                    id,
                    retweet_count,
                    time_zone,
                    lang
                )
                VALUES
                (
                    @timestamp,
                    @topic,	
                    @score,
                    @sentiment,
                    @text,
                    @id,
                    @retweet_count,
                    @time_zone,
                    @lang                
                )
            ",
            conn))
        {
            cmd.Parameters.AddWithValue("@timestamp",  new DateTime(1970, 1, 1).AddMilliseconds(tweet.timestamp_ms));
            cmd.Parameters.AddWithValue("@topic", tweet.topic);
            cmd.Parameters.AddWithValue("@score", tweet.score);
            cmd.Parameters.AddWithValue("@sentiment", tweet.sentiment);
            cmd.Parameters.AddWithValue("@text", tweet.text);
            cmd.Parameters.AddWithValue("@id", tweet.id);
            cmd.Parameters.AddWithValue("@retweet_count", tweet.retweet_count);
            cmd.Parameters.AddWithValue("@time_zone", tweet.time_zone);
            cmd.Parameters.AddWithValue("@lang", tweet.lang);

            cmd.ExecuteNonQuery();
        }
    }
    log.Info($"C# Event Hub trigger function processed a message: {eventHubMessage}");
}

private static async Task Score(Tweet tweet, TraceWriter log)
{
    var mlEndpoint = ConfigurationManager.AppSettings["MLEndpoint"];
    var payload = new {
        input_df = new [] {
            new {
                reviewText = tweet.text
            }
        }
    };

    var json = JsonConvert.SerializeObject(payload);

    log.Info(json);

    var client = new HttpClient();
    var content = new StringContent(json);
    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
    var response = await client.PostAsync(mlEndpoint, content);
    
    response.EnsureSuccessStatusCode();

    var responseString = await response.Content.ReadAsStringAsync();
    responseString = JsonConvert.DeserializeObject<string>(responseString);
    tweet.score = float.Parse(responseString);
    tweet.sentiment = GetSentimentFromScore(tweet.score);
}

private static string GetSentimentFromScore(float score)
{
    if (score < 0.4)
    {
        return "negative";
    }
    else if (score > 0.7)
    {
        return "positive";
    }
    else
    {
        return "neutral";
    }
}

private class Tweet
{
    public long id { get; set; }
    public long timestamp_ms { get; set; }
    public string topic { get; set; }
    public float score { get; set; }
    public string sentiment { get; set; }
    public string text { get; set; }
    public int retweet_count { get; set; }
    public string time_zone { get; set; }
    public string lang { get; set; }
}
