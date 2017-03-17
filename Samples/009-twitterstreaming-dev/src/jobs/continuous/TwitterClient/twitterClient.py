import sys, os, json, re
from tweepy import Stream
from tweepy import OAuthHandler
from tweepy.streaming import StreamListener
from azure.servicebus import ServiceBusService

class listener(StreamListener):
	whitespace_regex = re.compile('\s+')
	connection_string_regex = re.compile('^Endpoint=sb://(.+).servicebus.windows.net/;SharedAccessKeyName=(.+);SharedAccessKey=(.+);EntityPath=(.+)$')

	def __init__(self, event_hub_connection_string, keywords):
		m = self.connection_string_regex.match(event_hub_connection_string)
		self.sbs = ServiceBusService(
			m.group(1),
			shared_access_key_name=m.group(2),
			shared_access_key_value=m.group(3))
		self.keywords = keywords
		self.event_hub_name = m.group(4)

	def on_data(self, data):
		try:
			tweet = json.loads(data)
			p = re.compile('\s+')
			text =tweet['text']
			for topic in self.get_topics(text, keywords):
				output = json.dumps(
					{
						'text': text,
						'timestamp_ms': tweet['timestamp_ms'],
						'topic': topic
					})
				self.sbs.send_event(self.event_hub_name, output)
			return True
		except BaseException as e:
			print('failed ondata' + str(e))
			time.sleep(5)

	def on_error(self, status):
		print(status)

	def get_topics(self, input, keywords):
		return [keyword for keyword in self.keywords if self.sanitize_text(keyword) in self.sanitize_text(input)]

	def sanitize_text(self, input):
		return self.whitespace_regex.sub(' ', input.strip().lower())

keywords = os.environ["TWITTER_KEYWORDS"].split(",")
consumer_key_setting = os.environ["TWITTER_OAUTH_CONSUMER_KEY"]
consumer_secret_setting = os.environ["TWITTER_OAUTH_CONSUMER_SECRET"]
oauth_token_setting = os.environ["TWITTER_OAUTH_TOKEN"]
oauth_token_secret_setting = os.environ["TWITTER_OAUTH_TOKEN_SECRET"]
# TODO: need to fix connection string types in AzureFunctionApp. Using MYSQLCONNSTR_ for now,
# which is what they all default to.
# event_hub_connection_string = os.environ["CUSTOMCONNSTR_EventHubConnectionString"]
event_hub_connection_string = os.environ["MYSQLCONNSTR_EventHubConnectionString"]

auth = OAuthHandler(consumer_key_setting, consumer_secret_setting)
auth.set_access_token(oauth_token_setting, oauth_token_secret_setting)
twitterStream = Stream(auth, listener(event_hub_connection_string, keywords))
twitterStream.filter(track=keywords)
