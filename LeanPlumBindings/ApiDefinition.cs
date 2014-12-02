using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;

namespace LeanplumBindings
{
	public delegate void LeanplumStartBlock (bool success);
	public delegate void LeanplumVariablesChangedBlock ();
	public delegate bool LeanplumActionBlock (LPActionContext context);
	public delegate void LeanplumRegisterDeviceResponseBlock (string email);
	public delegate void LeanplumRegisterDeviceBlock (LeanplumRegisterDeviceResponseBlock response);
	public delegate void LeanplumRegisterDeviceFinishedBlock (bool success);
	public delegate void LeanplumHandleNotificationBlock ();
	public delegate void LeanplumShouldHandleNotificationBlock (NSDictionary userInfo, LeanplumHandleNotificationBlock response);
	public delegate UIBackgroundFetchResult LeanplumUIBackgroundFetchResult ();
	public delegate void LeanplumFetchCompletionBlock (LeanplumUIBackgroundFetchResult result);

	[BaseType (typeof (NSObject))]
	public partial interface Leanplum 
	{
		/// <summary>
		/// Optional. Sets the API server. The API path is of the form http[s]://hostname/servletName
		/// </summary>
		/// <param name="hostName">The name of the API host, such as www.leanplum.com.</param>
		/// <param name="servletName">The name of the API servlet, such as api.</param>
		/// <param name="ssl">Whether to use SSL.</param>
		[Static, Export ("setApiHostName:withServletName:usingSsl:")]
		void SetApiHostName (string hostName, string servletName, bool ssl);

		/// <summary>
		/// Optional. Adjusts the network timeouts.
		/// </summary>
		/// <param name="seconds">Timeout for requests in seconds. Default is 10.</param>
		[Static, Export ("setNetworkTimeoutSeconds:")]
		void SetNetworkTimeoutSeconds (int seconds);

		/// <summary>
		/// Optional. Adjusts the network timeouts.
		/// </summary>
		/// <param name="seconds">Timeout for requests in seconds. Default is 10.</param>
		/// <param name="downloadSeconds">Timeout for file downloads in seconds. Default is 15.</param>
		[Static, Export ("setNetworkTimeoutSeconds:forDownloads:")]
		void SetNetworkTimeoutSeconds (int seconds, int downloadSeconds);

		/// <summary>
		/// Advanced: Whether new variables can be downloaded mid-session. By default, this is disabled.
		/// Currently, if this is enabled, new variables can only be downloaded if a push notification is sent
		/// while the app is running, and the notification's metadata hasn't be downloaded yet.
		/// </summary>
		/// <param name="canDownload">If set to <c>true</c> can download new variables mid-session.</param>
		[Static, Export ("setCanDownloadContentMidSessionInProductionMode:")]
		void SetCanDownloadContentMidSessionInProductionMode (bool canDownload);

		/// <summary>
		/// Modifies the file hashing setting in development mode.
		/// By default, Leanplum will hash file variables to determine if they're modified and need
		/// to be uploaded to the server if we're running in the simulator.
		/// Setting this to NO will reduce startup latency in development mode, but it's possible
		/// that Leanplum will not always have the most up-to-date versions of your resources.
		/// </summary>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		[Static, Export ("setFileHashingEnabledInDevelopmentMode:")]
		void SetFileHashingEnabledInDevelopmentMode (bool enabled);

		// TODO: make internal and pass bool isProduction to call different implementations
		/// <summary>
		/// Must call either this or SetProductionAppId
		/// before issuing any calls to the API, including start.
		/// </summary>
		/// <param name="appId">Your app ID.</param>
		/// <param name="accessKey">Your development key.</param>
		[Static, Export ("setAppId:withDevelopmentKey:")]
		void SetDevelopmentAppId (string appId, string accessKey);

		// TODO: make internal and pass bool isProduction to call different implementations
		/// <summary>
		/// Must call either this or SetDevelopmentAppId
		/// before issuing any calls to the API, including start.
		/// </summary>
		/// <param name="appId">Your app ID.</param>
		/// <param name="accessKey">Your production key.</param>
		[Static, Export ("setAppId:withProductionKey:")]
		void SetProductionAppId (string appId, string accessKey);

		/// <summary>
		/// Sets a custom device ID. For example, you may want to pass the advertising ID to do attribution.
		/// By default, the device ID is the identifier for vendor.
		/// </summary>
		/// <param name="id">The device ID.</param>
		[Static, Export ("setDeviceId:")]
		void SetDeviceId (string id);

		/// <summary>
		/// Syncs resources between Leanplum and the current app.
		/// You should only call this once, and before Start.
		/// </summary>
		/// <param name = "async">Whether the call should be asynchronous</param>
		[Static, Export ("syncResources:")]
		void SyncResources (bool async);

		/// <summary>
		/// Syncs resources between Leanplum and the current app.
		/// You should only call this once, and before Start.
		/// </summary>
		/// <param name = "patternsToInclude">Limit paths
		/// to only those matching at least one pattern in this list.
		/// Supply null to indicate no inclusion patterns.
		/// Paths start with the folder name within the res folder,
		/// e.g. "layout/main.xml".</param>
		/// <param name = "patternsToExclude">Exclude paths
		/// matching at least one of these patterns.
		/// Supply null to indicate no exclusion patterns.</param>
		/// <param name = "async">Whether the call should be asynchronous</param>
		[Static, Export ("syncResourcePaths:excluding:async:")]
		void SyncResources (NSObject [] patternsToInclude, NSObject [] patternsToExclude, bool async);

		/// <summary>
		/// Call this when your application starts.
		/// This will initiate a call to Leanplum's servers to get the values
		/// of the variables used in your app
		/// </summary>
		[Static, Export ("start")]
		void Start ();

		/// <summary>
		/// Call this when your application starts.
		/// This will initiate a call to Leanplum's servers to get the values
		/// of the variables used in your app
		/// </summary>
		[Async]
		[Static, Export ("startWithResponseHandler:")]
		void Start (LeanplumStartBlock response);

		/// <summary>
		/// Call this when your application starts.
		/// This will initiate a call to Leanplum's servers to get the values
		/// of the variables used in your app
		/// </summary>
		[Static, Export ("startWithUserAttributes:")]
		void Start (NSDictionary attributes);

		/// <summary>
		/// Call this when your application starts.
		/// This will initiate a call to Leanplum's servers to get the values
		/// of the variables used in your app
		/// </summary>
		[Static, Export ("startWithUserId:")]
		void Start (string userId);

		/// <summary>
		/// Call this when your application starts.
		/// This will initiate a call to Leanplum's servers to get the values
		/// of the variables used in your app
		/// </summary>
		[Async]
		[Static, Export ("startWithUserId:responseHandler:")]
		void Start (string userId, LeanplumStartBlock response);

		/// <summary>
		/// Call this when your application starts.
		/// This will initiate a call to Leanplum's servers to get the values
		/// of the variables used in your app
		/// </summary>
		[Static, Export ("startWithUserId:userAttributes:")]
		void Start (string userId, NSDictionary attributes);

		/// <summary>
		/// Call this when your application starts.
		/// This will initiate a call to Leanplum's servers to get the values
		/// of the variables used in your app
		/// </summary>
		[Async]
		[Static, Export ("startWithUserId:userAttributes:responseHandler:")]
		void Start (string userId, NSDictionary attributes, LeanplumStartBlock response);

		/// <summary>
		/// Returns whether or not Leanplum has finished starting.
		/// </summary>
		[Static, Export ("hasStarted")]
		bool HasStarted { get; }

		/// <summary>
		/// Returns whether or not Leanplum has finished starting and the device is registered
		/// as a developer.
		/// </summary>
		[Static, Export ("hasStartedAndRegisteredAsDeveloper")]
		bool HasStartedAndRegisteredAsDeveloper { get; }

		// TODO: rework as event
		/// <summary>
		/// Block to call when the start call finishes, and variables are returned
		/// back from the server. Calling this multiple times will call each block
		/// in succession.
		/// </summary>
		/// <param name="block">Block to execute.</param>
		[Static, Export ("onStartResponse:")]
		void OnStartResponse (LeanplumStartBlock block);

		// TODO: rework as event
		/// <summary>
		/// Block to call when the variables receive new values from the server.
		/// This will be called on start, and also later on if the user is in an experiment
		/// that can update in realtime.
		/// </summary>
		/// <param name="block">Block to execute.</param>
		[Static, Export ("onVariablesChanged:")]
		void OnVariablesChanged (LeanplumVariablesChangedBlock block);

		// TODO: rework as event
		/// <summary>
		/// Block to call when no more file downloads are pending (either when
		/// no files needed to be downloaded or all downloads have been completed).
		/// </summary>
		/// <param name="block">Block to execute.</param>
		[Static, Export ("onVariablesChangedAndNoDownloadsPending:")]
		void OnVariablesChangedAndNoDownloadsPending (LeanplumVariablesChangedBlock block);

		// TODO: rework as event
		/// <summary>
		/// Block to call ONCE when no more file downloads are pending (either when
		/// no files needed to be downloaded or all downloads have been completed).
		/// </summary>
		/// <param name="block">Block to execute.</param>
		[Static, Export ("onceVariablesChangedAndNoDownloadsPending:")]
		void OnceVariablesChangedAndNoDownloadsPending (LeanplumVariablesChangedBlock block);

		/// <summary>
		/// Defines new action and message types to be performed at points set up on the Leanplum dashboard.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="kind">Kind.</param>
		/// <param name="args">Arguments.</param>
		[Static, Export ("defineAction:ofKind:withArguments:")]
		void DefineAction (string name, LeanplumActionKind kind, NSObject [] args);

		/// <summary>
		/// Defines new action and message types to be performed at points set up on the Leanplum dashboard.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="kind">Kind.</param>
		/// <param name="args">Arguments.</param>
		/// <param name="options">Options.</param>
		[Static, Export ("defineAction:ofKind:withArguments:withOptions:")]
		void DefineAction (string name, LeanplumActionKind kind, NSObject [] args, NSDictionary options);

		/// <summary>
		/// Defines new action and message types to be performed at points set up on the Leanplum dashboard.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="kind">Kind.</param>
		/// <param name="args">Arguments.</param>
		/// <param name="responder">Responder.</param>
		[Static, Export ("defineAction:ofKind:withArguments:withResponder:")]
		void DefineAction (string name, LeanplumActionKind kind, NSObject [] args, LeanplumActionBlock responder);

		/// <summary>
		/// Defines new action and message types to be performed at points set up on the Leanplum dashboard.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="kind">Kind.</param>
		/// <param name="args">Arguments.</param>
		/// <param name="options">Options.</param>
		/// <param name="responder">Responder.</param>
		[Static, Export ("defineAction:ofKind:withArguments:withOptions:withResponder:")]
		void DefineAction (string name, LeanplumActionKind kind, NSObject [] args, NSDictionary options, LeanplumActionBlock responder);

		// TODO: rework as event
		/// <summary>
		/// Block to call when an action is received, such as to show a message to the user.
		/// </summary>
		/// <param name="actionName">Action name.</param>
		/// <param name="block">Block.</param>
		[Static, Export ("onAction:invoke:")]
		void OnAction (string actionName, LeanplumActionBlock block);

		/// <summary>
		/// Handles a push notification for apps that use Background Notifications.
		/// Without background notifications, Leanplum handles them automatically.
		/// </summary>
		/// <param name="userInfo">User info.</param>
		/// <param name="completionHandler">Completion handler.</param>
		[Async]
		[Static, Export ("handleNotification:fetchCompletionHandler:")]
		void HandleNotification (NSDictionary userInfo, LeanplumFetchCompletionBlock completionHandler);

		/// <summary>
		/// Block to call that decides whether a notification should be displayed when it is
		/// received while the app is running, and the notification is not muted.
		/// Overrides the default behavior of showing an alert view with the notification message.
		/// </summary>
		/// <param name="block">Block.</param>
		[Static, Export ("setShouldOpenNotificationHandler")]
		void SetShouldOpenNotificationHandler (LeanplumShouldHandleNotificationBlock block);

		/// <summary>
		/// Adds a responder to be executed when an event happens.
		/// Similar to the methods above but uses NSInvocations instead of blocks.
		/// </summary>
		/// <param name="responder">Responder.</param>
		/// <param name="selector">Selector.</param>
		[Static, Export ("addStartResponseResponder:withSelector:")]
		void AddStartResponseResponder (NSObject responder, Selector selector);

		/// <summary>
		/// Adds a responder to be executed when an event happens.
		/// Similar to the methods above but uses NSInvocations instead of blocks.
		/// </summary>
		/// <param name="responder">Responder.</param>
		/// <param name="selector">Selector.</param>
		[Static, Export ("addVariablesChangedResponder:withSelector:")]
		void AddVariablesChangedResponder (NSObject responder, Selector selector);

		/// <summary>
		/// Adds a responder to be executed when an event happens.
		/// Similar to the methods above but uses NSInvocations instead of blocks.
		/// </summary>
		/// <param name="responder">Responder.</param>
		/// <param name="selector">Selector.</param>
		[Static, Export ("addVariablesChangedAndNoDownloadsPendingResponder:withSelector:")]
		void AddVariablesChangedAndNoDownloadsPendingResponder (NSObject responder, Selector selector);

		/// <summary>
		/// Adds a responder to be executed when an event happens.
		/// Similar to the methods above but uses NSInvocations instead of blocks.
		/// </summary>
		/// <param name="responder">Responder.</param>
		/// <param name="selector">Selector.</param>
		[Static, Export ("addResponder:withSelector:forActionNamed:")]
		void AddResponder (NSObject responder, Selector selector, string actionName);

		/// <summary>
		/// Adds a responder to be executed when an event happens.
		/// Similar to the methods above but uses NSInvocations instead of blocks.
		/// </summary>
		/// <param name="responder">Responder.</param>
		/// <param name="selector">Selector.</param>
		[Static, Export ("removeStartResponseResponder:withSelector:")]
		void RemoveStartResponseResponder (NSObject responder, Selector selector);

		/// <summary>
		/// Adds a responder to be executed when an event happens.
		/// Similar to the methods above but uses NSInvocations instead of blocks.
		/// </summary>
		/// <param name="responder">Responder.</param>
		/// <param name="selector">Selector.</param>
		[Static, Export ("removeVariablesChangedResponder:withSelector:")]
		void RemoveVariablesChangedResponder (NSObject responder, Selector selector);

		/// <summary>
		/// Adds a responder to be executed when an event happens.
		/// Similar to the methods above but uses NSInvocations instead of blocks.
		/// </summary>
		/// <param name="responder">Responder.</param>
		/// <param name="selector">Selector.</param>
		[Static, Export ("removeVariablesChangedAndNoDownloadsPendingResponder:withSelector:")]
		void RemoveVariablesChangedAndNoDownloadsPendingResponder (NSObject responder, Selector selector);

		/// <summary>
		/// Adds a responder to be executed when an event happens.
		/// Similar to the methods above but uses NSInvocations instead of blocks.
		/// </summary>
		/// <param name="responder">Responder.</param>
		/// <param name="selector">Selector.</param>
		[Static, Export ("removeResponder:withSelector:forActionNamed:")]
		void RemoveResponder (NSObject responder, Selector selector, string actionName);

		/// <summary>
		/// Sets additional user attributes after the session has started.
		/// Variables retrieved by start won't be targeted based on these attributes, but
		/// they will count for the current session for reporting purposes.
		/// Only those attributes given in the dictionary will be updated. All other
		/// attributes will be preserved.
		/// </summary>
		/// <param name="attributes">Attributes.</param>
		[Static, Export ("setUserAttributes:")]
		void SetUserAttributes (NSDictionary attributes);

		/// <summary>
		/// Updates a user ID after session start.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		[Static, Export ("setUserId:")]
		void SetUserId (string userId);

		/// <summary>
		/// Updates a user ID after session start with a dictionary of user attributes.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <param name="attributes">Attributes.</param>
		[Static, Export ("setUserId:withUserAttributes:")]
		void SetUserId (string userId, NSDictionary attributes);

		/// <summary>
		/// Advances to a particular state in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// A state is a section of your app that the user is currently in.
		/// </summary>
		/// <param name="state">The name of the state.</param>
		[Static, Export ("advanceTo:")]
		void AdvanceTo (string state);

		/// <summary>
		/// Advances to a particular state in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// A state is a section of your app that the user is currently in.
		/// </summary>
		/// <param name="state">The name of the state.</param>
		/// <param name = "info">Anything else you want to log with the state. For example, if the state
		/// is watchVideo, info could be the video ID</param>
		[Static, Export ("advanceTo:withInfo:")]
		void AdvanceTo (string state, string info);

		/// <summary>
		/// Advances to a particular state in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// A state is a section of your app that the user is currently in.
		/// </summary>
		/// <param name="state">The name of the state.</param>
		/// <param name="parameters">A dictionary with custom parameters.</param>
		[Static, Export ("advanceTo:withParameters:")]
		void AdvanceTo (string state, NSDictionary parameters);

		/// <summary>
		/// Advances to a particular state in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// A state is a section of your app that the user is currently in.
		/// </summary>
		/// <param name="state">The name of the state.</param>
		/// <param name="info">Anything else you want to log with the state. For example, if the state
		/// is watchVideo, info could be the video ID.</param>
		/// <param name="parameters">A dictionary with custom parameters.</param>
		[Static, Export ("advanceTo:withInfo:andParameters:")]
		void AdvanceTo (string state, string info, NSDictionary parameters);

		/// <summary>
		/// Pauses the current state.
		/// You can use this if your game has a "pause" mode. You shouldn't call it
		/// when someone switches out of your app because that's done automatically.
		/// </summary>
		[Static, Export ("pauseState")]
		void PauseState ();

		/// <summary>
		/// Resumes the current state.
		/// </summary>
		[Static, Export ("resumeState")]
		void ResumeState ();

		/// <summary>
		/// Automatically tracks all of the screens in the app as states.
		/// You should not use this in conjunction with advanceTo as the user can only be in
		/// 1 state at a time.
		/// </summary>
		[Static, Export ("trackAllAppScreens")]
		void TrackAllAppScreens ();

		/// <summary>
		/// Logs a particular event in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// </summary>
		/// <param name="eventToTrack">Event to track.</param>
		[Static, Export ("track:")]
		void Track (string eventToTrack);

		/// <summary>
		/// Logs a particular event in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// </summary>
		/// <param name="eventToTrack">Event to track.</param>
		/// <param name="value">Value.</param>
		[Static, Export ("track:withValue:")]
		void Track (string eventToTrack, double value);

		/// <summary>
		/// Logs a particular event in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// </summary>
		/// <param name="eventToTrack">Event to track.</param>
		/// <param name="info">Info.</param>
		[Static, Export ("track:withInfo:")]
		void Track (string eventToTrack, string info);

		/// <summary>
		/// Logs a particular event in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// </summary>
		/// <param name="eventToTrack">Event to track.</param>
		/// <param name="value">Value.</param>
		/// <param name="info">Info.</param>
		[Static, Export ("track:withValue:andInfo:")]
		void Track (string eventToTrack, double value, string info);

		/// <summary>
		/// Logs a particular event in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// </summary>
		/// <param name="eventToTrack">Event to track.</param>
		/// <param name="parameters">Parameters.</param>
		[Static, Export ("track:withParameters:")]
		void Track (string eventToTrack, NSDictionary parameters);

		/// <summary>
		/// Logs a particular event in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// </summary>
		/// <param name="eventToTrack">Event to track.</param>
		/// <param name="value">Value.</param>
		/// <param name="parameters">Parameters.</param>
		[Static, Export ("track:withValue:andParameters:")]
		void Track (string eventToTrack, double value, NSDictionary parameters);

		/// <summary>
		/// Logs a particular event in your application. The string can be
		/// any value of your choosing, and will show up in the dashboard.
		/// </summary>
		/// <param name="eventToTrack">Event to track.</param>
		/// <param name="value">Value.</param>
		/// <param name="info">Info.</param>
		/// <param name="parameters">Parameters.</param>
		[Static, Export ("track:withValue:andInfo:andParameters:")]
		void Track (string eventToTrack, double value, string info, NSDictionary parameters);

		/// <summary>
		/// Gets the path for a particular resource. The resource can be overridden by the server.
		/// </summary>
		/// <returns>The for resource.</returns>
		/// <param name="name">Name.</param>
		/// <param name="extension">Extension.</param>
		[Static, Export ("pathForResource:ofType:")]
		string PathForResource (string name, string extension);

		[Static, Export ("objectForKeyPath:")]
		NSObject ObjectForKeyPath (NSObject firstComponent);

		[Static, Export ("objectForKeyPathComponents:")]
		NSObject ObjectForKeyPathComponents (NSObject [] pathComponents);

		/// <summary>
		/// Gets a list of variants that are currently active for this user.
		/// Each variant is a dictionary containing an id.
		/// </summary>
		/// <value>The variants.</value>
		[Static, Export ("variants")]
		NSObject [] Variants { get; }

		/// <summary>
		/// Forces content to update from the server. If variables have changed, the
		/// appropriate callbacks will fire. Use sparingly as if the app is updated,
		/// you'll have to deal with potentially inconsistent state or user experience.
		/// </summary>
		[Static, Export ("forceContentUpdate")]
		void ForceContentUpdate ();

		/// <summary>
		/// Forces content to update from the server. If variables have changed, the
		/// appropriate callbacks will fire. Use sparingly as if the app is updated,
		/// you'll have to deal with potentially inconsistent state or user experience.
		/// The provided callback will always fire regardless
		/// of whether the variables have changed.
		/// </summary>
		/// <param name="block">Block.</param>
		[Static, Export ("forceContentUpdate:")]
		[Async]
		void ForceContentUpdate (LeanplumVariablesChangedBlock block);

		/// <summary>
		/// This should be your first statement in a unit test. This prevents
		/// Leanplum from communicating with the server.
		/// </summary>
		[Static, Export ("enableTestMode")]
		void EnableTestMode ();
	}

	[BaseType (typeof (NSObject))]
	public partial interface LeanplumCompatibility 
	{
		/// <summary>
		/// Used only for compatibility with Google Analytics.
		/// </summary>
		/// <param name="trackingObject">Tracking object.</param>
		[Static, Export ("gaTrack:")]
		void GaTrack (NSObject trackingObject);
	}

	// TODO: rework as event
	[Model, BaseType (typeof (NSObject))]
	public partial interface LPVarDelegate 
	{
		/// <summary>
		/// For file variables, called when the file is ready.
		/// </summary>
		/// <param name="variable">Variable.</param>
		[Export ("fileIsReady:")]
		void  FileIsReady(LPVar variable);

		/// <summary>
		/// Called when the value of the variable changes.
		/// </summary>
		/// <param name="variable">Variable.</param>
		[Export ("valueDidChange:")]
		void  ValueChanged(LPVar variable);
	}

	/// <summary>
	/// A variable is any part of your application that can change from an experiment.
	/// </summary>
	[BaseType (typeof (NSObject))]
	public partial interface LPVar 
	{
		/// <summary>
		/// Defines a LPVar.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		[Static, Export ("define:")]
		LPVar Define (string name);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withInt:")]
		LPVar Define (string name, int defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withFloat:")]
		LPVar Define (string name, float defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withDouble:")]
		LPVar Define (string name, double defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withShort:")]
		LPVar Define (string name, short defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withChar:")]
		LPVar Define (string name, sbyte defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withBool:")]
		LPVar Define (string name, bool defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withString:")]
		LPVar Define (string name, string defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withNumber:")]
		LPVar Define (string name, NSNumber defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withLongLong:")]
		LPVar Define (string name, long defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withUnsignedChar:")]
		LPVar Define (string name, byte defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withUnsignedInt:")]
		LPVar Define (string name, uint defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withUnsignedLongLong:")]
		LPVar Define (string name, ulong defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withUnsignedShort:")]
		LPVar Define (string name, ushort defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultFilename">Default value.</param>
		[Static, Export ("define:withFile:")]
		LPVar DefineWithFile (string name, string defaultFilename);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withDictionary:")]
		LPVar Define (string name, NSDictionary defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withArray:")]
		LPVar Define (string name, NSObject [] defaultValue);

		/// <summary>
		/// Define a LPVar with the specified name and a defaultValue.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("define:withColor:")]
		LPVar Define (string name, UIColor defaultValue);

		/// <summary>
		/// Returns the name of the variable
		/// </summary>
		/// <value>The name.</value>
		[Export ("name")]
		string Name { get; }

		/// <summary>
		/// Returns the components of the variable's name.
		/// </summary>
		/// <value>The name components.</value>
		[Export ("nameComponents")]
		NSString [] NameComponents { get; }

		/// <summary>
		/// Returns the default value of a variable.
		/// </summary>
		/// <value>The default value.</value>
		[Export ("defaultValue")]
		NSObject DefaultValue { get; }

		/// <summary>
		/// Returns the kind of the variable.
		/// </summary>
		/// <value>The kind.</value>
		[Export ("kind")]
		string Kind { get; }

		/// <summary>
		/// Returns whether the variable has changed since the last time the app was run.
		/// </summary>
		/// <value><c>true</c> if this instance has changed; otherwise, <c>false</c>.</value>
		[Export ("hasChanged")]
		bool HasChanged { get; }

		// TODO: rework as event
		/// <summary>
		/// For file variables, called when the file is ready.
		/// </summary>
		/// <param name="block">Block.</param>
		[Export ("onFileReady:")]
		void OnFileReady (LeanplumVariablesChangedBlock block);

		// TODO: rework as event
		/// <summary>
		/// Called when the value of the variable changes.
		/// </summary>
		/// <param name="block">Block.</param>
		[Export ("onValueChanged:")]
		void OnValueChanged (LeanplumVariablesChangedBlock block);

		/// <summary>
		/// Sets the delegate of the variable in order to use 
		/// FileIsReady} and ValueChanged
		/// </summary>
		/// <param name="del">The delegate.</param>
		[Export ("setDelegate:")]
		void SetDelegate (LPVarDelegate del);

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <returns>The for key.</returns>
		/// <param name="key">Key.</param>
		[Export ("objectForKey:")]
		NSObject ObjectForKey (string key);

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <returns>The at index.</returns>
		/// <param name="index">Index.</param>
		[Export ("objectAtIndex:")]
		NSObject ObjectAtIndex (uint index);

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <returns>The for key path.</returns>
		/// <param name="firstComponent">First component.</param>
		[Export ("objectForKeyPath:")]
		NSObject ObjectForKeyPath (NSObject firstComponent);

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <returns>The for key path components.</returns>
		/// <param name="pathComponents">Path components.</param>
		[Export ("objectForKeyPathComponents:")]
		NSObject ObjectForKeyPathComponents (NSObject [] pathComponents);

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The count.</value>
		[Export ("count")]
		uint Count { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The number value.</value>
		[Export ("numberValue")]
		NSNumber NumberValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The string value.</value>
		[Export ("stringValue")]
		string StringValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The file value.</value>
		[Export ("fileValue")]
		string FileValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The image value.</value>
		[Export ("imageValue")]
		UIImage ImageValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The int value.</value>
		[Export ("intValue")]
		int IntValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The double value.</value>
		[Export ("doubleValue")]
		double DoubleValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The cg float value.</value>
		[Export ("cgFloatValue")]
		float CgFloatValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The float value.</value>
		[Export ("floatValue")]
		float FloatValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The short value.</value>
		[Export ("shortValue")]
		short ShortValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value><c>true</c> if bool value; otherwise, <c>false</c>.</value>
		[Export ("boolValue")]
		bool BoolValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The char value.</value>
		[Export ("charValue")]
		sbyte CharValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The long value.</value>
		[Export ("longValue")]
		int LongValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The long long value.</value>
		[Export ("longLongValue")]
		long LongLongValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The integer value.</value>
		[Export ("integerValue")]
		int IntegerValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The unsigned char value.</value>
		[Export ("unsignedCharValue")]
		byte UnsignedCharValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The unsigned short value.</value>
		[Export ("unsignedShortValue")]
		ushort UnsignedShortValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The unsigned int value.</value>
		[Export ("unsignedIntValue")]
		uint UnsignedIntValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The unsigned integer value.</value>
		[Export ("unsignedIntegerValue")]
		uint UnsignedIntegerValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The unsigned long value.</value>
		[Export ("unsignedLongValue")]
		uint UnsignedLongValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.s
		/// </summary>
		/// <value>The unsigned long long value.</value>
		[Export ("unsignedLongLongValue")]
		ulong UnsignedLongLongValue { get; }

		/// <summary>
		/// Accessess the value(s) of the variable.
		/// </summary>
		/// <value>The color value.</value>
		[Export ("colorValue")]
		UIColor ColorValue { get; }
	}

	[BaseType (typeof (NSObject))]
	public partial interface LPActionArg 
	{
		/// <summary>
		/// Defines a Leanplum Action Argument.
		/// </summary>
		/// <returns>The named.</returns>
		/// <param name="name">Name.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("argNamed:withNumber:")]
		LPActionArg ArgNamed (string name, NSNumber defaultValue);

		/// <summary>
		/// Defines a Leanplum Action Argument.
		/// </summary>
		/// <returns>The named.</returns>
		/// <param name="name">Name.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("argNamed:withString:")]
		LPActionArg ArgNamed (string name, string defaultValue);

		/// <summary>
		/// Defines a Leanplum Action Argument.
		/// </summary>
		/// <returns>The named.</returns>
		/// <param name="name">Name.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("argNamed:withBool:")]
		LPActionArg ArgNamed (string name, bool defaultValue);

		/// <summary>
		/// Defines a Leanplum Action Argument.
		/// </summary>
		/// <returns>The named.</returns>
		/// <param name="name">Name.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("argNamed:withFile:")]
		LPActionArg ArgNamedWithFile (string name, string defaultValue);

		/// <summary>
		/// Defines a Leanplum Action Argument.
		/// </summary>
		/// <returns>The named.</returns>
		/// <param name="name">Name.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("argNamed:withDict:")]
		LPActionArg ArgNamed (string name, NSDictionary defaultValue);

		/// <summary>
		/// Defines a Leanplum Action Argument.
		/// </summary>
		/// <returns>The named.</returns>
		/// <param name="name">Name.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("argNamed:withArray:")]
		LPActionArg ArgNamed (string name, NSObject [] defaultValue);

		/// <summary>
		/// Defines a Leanplum Action Argument.
		/// </summary>
		/// <returns>The named.</returns>
		/// <param name="name">Name.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("argNamed:withAction:")]
		LPActionArg ArgNamedWithAction (string name, string defaultValue);

		/// <summary>
		/// Defines a Leanplum Action Argument.
		/// </summary>
		/// <returns>The named.</returns>
		/// <param name="name">Name.</param>
		/// <param name="defaultValue">Default value.</param>
		[Static, Export ("argNamed:withColor:")]
		LPActionArg ArgNamed (string name, UIColor defaultValue);

		/// <summary>
		/// Gets the name of the argument.
		/// </summary>
		/// <value>The name.</value>
		[Export ("name")]
		string Name { get; }

		/// <summary>
		/// Gets the kind of the argument.
		/// </summary>
		/// <value>The kind.</value>
		[Export ("kind")]
		string Kind { get; }

		/// <summary>
		/// Gets the default value of the argument.
		/// </summary>
		/// <value>The default value.</value>
		[Export ("defaultValue")]
		NSObject DefaultValue { get; }
	}

	[BaseType (typeof (NSObject))]
	public partial interface LPActionContext {

		[Export ("actionName")]
		string ActionName { get; }

		[Export ("stringNamed:")]
		string StringNamed (string name);

		[Export ("fileNamed:")]
		string FileNamed (string name);

		[Export ("numberNamed:")]
		NSNumber NumberNamed (string name);

		[Export ("boolNamed:")]
		bool BoolNamed (string name);

		[Export ("dictionaryNamed:")]
		NSDictionary DictionaryNamed (string name);

		[Export ("arrayNamed:")]
		NSObject [] ArrayNamed (string name);

		[Export ("colorNamed:")]
		UIColor ColorNamed (string name);

		/// <summary>
		/// Runs the action given by the "name" key.
		/// </summary>
		/// <param name="name">Name.</param>
		[Export ("runActionNamed:")]
		void RunActionNamed (string name);

		/// <summary>
		/// Runs and tracks an event for the action given by the "name" key.
		/// This will track an event if no action is set.
		/// </summary>
		/// <param name="name">Name.</param>
		[Export ("runTrackedActionNamed:")]
		void RunTrackedActionNamed (string name);

		/// <summary>
		/// Tracks an event in the context of the current message.
		/// </summary>
		/// <param name="eventToTrack">Event to track.</param>
		/// <param name="value">Value.</param>
		/// <param name="parameters">Parameters.</param>
		[Export ("track:withValue:andParameters:")]
		void Track (string eventToTrack, double value, NSDictionary parameters);

		/// <summary>
		/// Prevents the currently active message from appearing again in the future.
		/// </summary>
		[Export ("muteFutureMessagesOfSameKind")]
		void MuteFutureMessagesOfSameKind ();
	}

	// TODO: generate bindings for LPMessageTemplates
}