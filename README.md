1. User is identified by:
	a. A group ID, which is set in the 
	b. A nickname, which must be unique in a group.
2. A message received by the server has the following attributes (besides the message itself):
	a. A unique identifier (called GUID, global unique identifier).
	b. The time that it was received by the server.
	c. The user information.
	d. The message body.
Functional Requirements
	1. A client command-line interface (CLI) that will support the following operations:
		a. Registration.
		b. Login/Logout.
		c. Retrieve last 10 messages from server.
		d. Display last 20 retrieved messages (without retrieving new ones from the server), sorted by the message timestamp.
		e.Display all retrieved messages (without retrieving new ones from the server) written by a certain user (identified by a username and a group id), sorted by the message timestamp.
		f. Write (and send) a new message (max. Length 150 characters).
		g. Exit (logout first).
	2. Messages displayed on screen should include all message details received from the server.
Non-functional Requirements
	1. Persistency:
		a. In this assignment, persistent data is stored in local files.
		b. The following data should be persistent:
			i.Users data.
			ii. Received messages (including messages returned by calling Communication.Instance.Send(), see below.
		c. Persistent data should be restored once the CLI client starts.
	2.Logging:
		You must maintain a log which will track all errors in the system. Note that “error” does not necessarily mean an exception (see next item) that is “thrown” or “raised” by your runtime environment, but any situation which counts as invalid in our domain. Some guidelines:
		a. Tag entries with their severity / priority
		b. Provide enough information to understand what went wrong, and where
		c. Avoid storing entire stack traces directly in the log (they are more verbose than useful)
		d. Use a shelf Logger. Some
		examples:
​		log4net, Observer logger, Composition Logger, end there are many more.
	3. Exception Handling is the process of responding to the occurrence, during computation, of exceptions – anomalous or exceptional conditions requiring special processing – often changing the normal flow of program execution. Your program is expected to operate even when error occurs:
		a. Handle any malformed input.
		b. Handle logic errors (e.g., login for non-existing user, etc.).
