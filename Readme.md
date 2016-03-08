# PiGit

This application allows you to send notifications from a 
given service (Bitbucket, Github, Slack, you name it) to any
of your Windows 10 devices using SignalR and Webhooks, 
and based on that notification it plays a sound.

I personally use a Raspberry Pi 2 to notify me when somebody has made any changes
on the repository my team is working on.

## The workflow

1. Any service calls the websever via webhooks. In this application
I implemented a BitBucket Webhook.
2. The webserver parses the message and sends it to the client(s) using SignalR.
3. The clients listen for incoming messages and based on that it plays a sound.

## Setup steps

1. Implement your webhook of choice. 
(If you want to use something else then BitBucket). 
I can strongly suggest using the [ASP.NET Webhooks](https://github.com/aspnet/WebHooks) or reading [this](http://www.hanselman.com/blog/IntroducingASPNETWebHooksReceiversWebHooksMadeEasy.aspx) blog post
2. Take the lightweight webserver, and publish it to any cloud service you prefer.
3. Generate an SHA256 key. You can easily find some [online generator](http://www.xorbin.com/tools/sha256-hash-calculator) for it.
4. Add an application settings with a key of "MS_WebHookReceiverSecret_Bitbucket" and a value of the generated SHA256.
5. Go to your service of choice and probably under settings/webhooks you can enter the URL of your published webserver {url_of_your_webserver}/api/webhooks/incoming/{servicename}/?code={generatedkey}
6. At this point your service of choice and your webserver is connected. Now we have to setup the connection between the clients and the webserver. For this we need to set the Common Settings.cs properties
7. In Settings.cs set the `HubUrl` to your webserver URL. The `RestrictedGroupName` you can use to restrict who is going to be notified. If you choose not to use this property then anybody can subscribe to your SignalR hub, and can receive your notifications.
With this method you can restrict that only you get the message. I suggest to generate another SHA256 key and use it as a `RestrictedGroupName`.

The UWP application maps the message (action) from the server to a folder which contains x number of .wav files. The logic implemented in the UWP client picks one using a basic randomization logic and plays it.