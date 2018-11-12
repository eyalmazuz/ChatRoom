# ChatRoom

chatroom project for Intro to Software Enginnering course. the ChatRoom software uses the Multitier architecture model. in the project we experienced full software development cycle from, client requirements to high-level design to low-level design to creating the software, we had to deal with sudden changes in the software according to the client needs and use testing methodologies to test our software.

the software used the 4 layer architecture:

Presentation layer- the GUI of the sofware

Application layer- the massanger between the Presentation layer and the Logic layer uses MVVM architecture pattern

Business layer- incharge of all the logics behind the scenes of the software and accessing the Data access layer

Data access layer- data layer which stores all the logs of the software and the code that wrote all the data to the SQL data base that was used in the project
## Milestones:

the project was divided into 3 milestones
### Milestone 1:

In this milestone, you are requested to implement a CLI desktop client for a chatroom. Your client will connect and communicate with a server that we have deployed for you. Your client will retrieve messages from the server, display and write messages, and handle users login and registration. Your client will have three layers constructed by you - presentation layer (CLI), logic layer, and persistent layer. In addition we will supply a communication layer for your client in order to communicate with the server.

### Milestone 2:

After the success of the first milestone, you are now requested to further expand the functionality of your chat-room client. After using your chat room client for several months your clients complain that interactions with your client are too cumbersome (difficult). To improve this, they ask you to build a pretty graphical user interface (GUI) for it. This interface should support all of the existing functionality (with some changes, see below). Additionally, the client will automatically pull new messages, update the view, and support filtering and sorting the messages. You and your team are tasked to upgrade your current chat-room client and generate a new version that supports these new demands.

### Milestone 3:

The last two versions of your Chatroom client was successful and your users are satisfied, though they wish to retrieve messages that were sent while they were offline. Recently, there has been an upgrade to the server and you must adapt your client to comply with these changes, as well as to take advantage of the new capabilities that the server is now providing. The upgrade replaced the former python server and replaced it with a central SQL server. The server stores the users and messages details, and allows for viewing and manipulating the its data. To put in plainly, your client’s communication layer is no longer needed. In addition, your data access layer will read/write users and messages from/to the central SQL server (instead of keeping them in files). This milestone adds some new and exciting functionalities to your Chatroom Client to make it more appealing.
