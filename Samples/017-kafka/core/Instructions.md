## Instructions

#### Kafka Cluster
[Open the Kafka Cluster](https://portal.azure.com/#resource/subscriptions/{SubscriptionId}/resourceGroups/{ResourceGroup.Name}/providers/Microsoft.HDInsight/clusters/{Outputs.kafkaClusterName}/overview)

#### Spark Cluster
[Open the Spark Cluster](https://portal.azure.com/#resource/subscriptions/{SubscriptionId}/resourceGroups/{ResourceGroup.Name}/providers/Microsoft.HDInsight/clusters/{Outputs.sparkClusterName}/overview)

## Next steps

## Use Spark Structured Streaming with Kafka (preview) on HDInsight

#### Get the Kafka brokers

The code in this example connects to the Kafka broker hosts in the Kafka cluster. To find the Kafka broker hosts, use the following PowerShell or Bash example:

```powershell
$creds = Get-Credential -UserName "admin" -Message "Enter the HDInsight login"
$resp = Invoke-WebRequest -Uri "https://{Outputs.KAFKACLUSTERNAME}.azurehdinsight.net/api/v1/clusters/{Outputs.KAFKACLUSTERNAME}/services/KAFKA/components/KAFKA_BROKER" `
    -Credential $creds
$respObj = ConvertFrom-Json $resp.Content
$brokerHosts = $respObj.host_components.HostRoles.host_name
($brokerHosts -join ":9092,") + ":9092"
```

```bash
curl -u admin:$PASSWORD -G "https://{Outputs.KAFKACLUSTERNAME}.azurehdinsight.net/api/v1/clusters/{Outputs.KAFKACLUSTERNAME}/services/KAFKA/components/KAFKA_BROKER" | jq -r '["\(.host_components[].HostRoles.host_name):9092"] | join(",")'
```

> [!NOTE]
> This example expects `$PASSWORD` to contain the password for the cluster login.
>
> This example uses the [jq](https://stedolan.github.io/jq/) utility to parse data out of the JSON document.

The output is similar to the following text:

`wn0-kafka.0owcbllr5hze3hxdja3mqlrhhe.ex.internal.cloudapp.net:9092,wn1-kafka.0owcbllr5hze3hxdja3mqlrhhe.ex.internal.cloudapp.net:9092,wn2-kafka.0owcbllr5hze3hxdja3mqlrhhe.ex.internal.cloudapp.net:9092,wn3-kafka.0owcbllr5hze3hxdja3mqlrhhe.ex.internal.cloudapp.net:9092`

Save this information, as it is used in the following sections of this document.

#### Get the notebooks

The code for the example described in this document is available at [https://github.com/Azure-Samples/hdinsight-spark-kafka-structured-streaming](https://github.com/Azure-Samples/hdinsight-spark-kafka-structured-streaming).

#### Upload the notebooks

Use the following steps to upload the notebooks from the project to your Spark on HDInsight cluster:

1. In your web browser, connect to the Jupyter notebook on your Spark cluster. In the following URL, replace `CLUSTERNAME` with the name of your Spark cluster:

        https://{Outputs.SPARKCLUSTERNAME}.azurehdinsight.net/jupyter

    When prompted, enter the cluster login and password used when you created the cluster.

2. From the upper right side of the page, use the __Upload__ button to upload the __Stream-Tweets-To_Kafka.ipynb__ file to the cluster. Select __Open__ to start the upload.

3. Find the __Stream-Tweets-To_Kafka.ipynb__ entry in the list of notebooks, and select __Upload__ button beside it.

4. Repeat steps 1-3 to load the __Spark-Structured-Streaming-From-Kafka.ipynb__ notebook.

#### Load tweets into Kafka

Once the files have been uploaded, select the __Stream-Tweets-To_Kafka.ipynb__ entry to open the notebook. Follow the steps in the notebook to load tweets into Kafka.

#### Process tweets using Spark Structured Streaming

From the Jupyter Notebook home page, select the __Spark-Structured-Streaming-From-Kafka.ipynb__ entry. Follow the steps in the notebook to load tweets from Kafka using Spark Structured Streaming.

## Apache Spark streaming (DStream) example with Kafka (preview) on HDInsight

Learn how to use Spark Apache Spark to stream data into or out of Apache Kafka on HDInsight using DStreams. This example uses a Jupyter notebook that runs on the Spark cluster.

#### Use the notebooks

The code for the example described in this document is available at [https://github.com/Azure-Samples/hdinsight-spark-scala-kafka](https://github.com/Azure-Samples/hdinsight-spark-scala-kafka).

Follow the steps below or in the `README.md` file to complete this example.

#### To run this example

To use the example Jupyter notebooks, you must upload them to the Jupyter Notebook server on the Spark cluster. Use the following steps to upload the notebook:

1. In your web browser, use the following URL to connect to the Jupyter Notebook server on the Spark cluster. Replace __CLUSTERNAME__ with the name of your Spark cluster.

        https://{Outputs.SPARKCLUSTERNAME}.azurehdinsight.net/jupyter

    When prompted, enter the cluster login (admin) and password used when you created the cluster.

2. From the upper right side of the page, use the __Upload__ button to upload the `Stream-Tweets-To-Kafka.ipynb` file. Select the file in the file browser dialog and select __Open__. 

3. Find the __Stream-Tweets-To-Kafka.ipynb__ entry in the list of notebooks, and select __Upload__ button beside it.

4. Once the file has uploaded, select the __KafkaStreaming.ipynb__ entry to open the notebook. To load tweets into Kafka, follow the instructions in the notebook.

5. Repeat steps 1-3 to upload the `Spark-Streaming-From-Kafka-With-DStreams.ipynb` document to Kafka. Once the file has uploaded, select the entry to open the notebook. Follow the instructions in the notebook to read the tweets from Kafka.


#### Streaming Quick links

* [Use Spark Structured Streaming with Kafka (preview) on HDInsight](https://docs.microsoft.com/en-us/azure/hdinsight/hdinsight-apache-kafka-spark-structured-streaming#get-the-kafka-brokers)
* [Stream-Tweets-To-Kafka Notebooks](https://github.com/Azure-Samples/hdinsight-spark-kafka-structured-streaming)

* [Apache Spark streaming (DStream) example with Kafka (preview) on HDInsight](https://docs.microsoft.com/en-us/azure/hdinsight/hdinsight-apache-spark-with-kafka#use-the-notebooks)
* [KafkaStreaming Notebook](https://github.com/Azure-Samples/hdinsight-spark-scala-kafka)