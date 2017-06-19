This sample demonstrates how to use Spark with Kafka (preview) on HDInsight

While you can create an Azure virtual network, Kafka, and Spark clusters manually, it's easier to use an Azure Resource Manager template. 
Use the following steps to deploy an Azure virtual network, Kafka, and Spark clusters to your Azure subscription.

The pattern involves the following steps:

1. Deploy an ARM template which creates the following resources:
	* Kafka on HDInsight 3.5 cluster
	* Spark on HDInsight 3.6 cluster
	* Azure Virtual Network, which contains the HDInsight clusters
	
2. [Use Spark Structured Streaming with Kafka (preview) on HDInsight](https://docs.microsoft.com/en-us/azure/hdinsight/hdinsight-apache-kafka-spark-structured-streaming#get-the-kafka-brokers)

3. [Apache Spark streaming (DStream) example with Kafka (preview) on HDInsight](https://docs.microsoft.com/en-us/azure/hdinsight/hdinsight-apache-spark-with-kafka#a-idkafkahostsakafka-host-information)

