## About #tweetyzard

It's a Twitter data gathering tool which uses Twitter Stream and Search API. This tool supports exporting of the gathered tweets in a excel file and exporting of data to a database.  These gathered data further can be used for various social media analytics.

This tool uses  tweetinvi for tweeter related operations and for the presentation layer it uses MetroFramework. This application is in its very initial stage but satisfies the basic need for searching and saving tweets for a given search phrase.

#### What to Expect?

![alt text](https://pbs.twimg.com/media/B3qTTIiCUAAFOdc.png)

1. Trending now: It shows worldwide Twitter trends, these can be used for search directly.

2. Search using Search API: The magnifying glass button searches tweets for last 7 days on a given keyword.

3. Live stream of tweets using Stream API: The play button streams twitter in real time, optionally it can gather geo tagged tweets only.

4. Export to Excel: Export to excel button exports all gathered tweets in a excel file.

5. Export to Database:  Export to database button exports all gathered tweets to a database table.

6. New Addition : Save the streaming data directly to database. see the documentation.

## Setup Instruction

#### Setup application keys:

Create your twitter app and get the secret keys form https://apps.twitter.com/ and edit the App.Config of Presentation/tweetyzard project and change the below keys:

<add key="AccessToken" value="YourAppAccessToken" />
<add key="AccessTokenSecret" value="YourAppAccessTokenSecret" />
<add key="ConsumerKey" value="YourAppConsumerKey" />
<add key="ConsumerSecret" value="YourAppConsumerSecret" />

#### Setup Database:

Publish the Database\tweetyzard.database project to a SQL server database and change the below key accordingly:

<add key="DbConnectionString" value="data source=.;initial catalog=tweetyzard.database;integrated security=True" /> 

Saving stream directly to database:

A new functionality has been added where we can directly save the streaming data in database without stopping the stream. To enable this feature download the source code from the source code tab and then turn <add key="SaveStreamToDatabase" value="false"/> to true.

## SYSTEM REQUIREMENT
#### FOR USE

.NET Framework 4.5
#### FOR DEVELOPMENT

Visual Studio 2012
Sql Server 2012
Sql Server Data tools

