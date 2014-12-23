LeanplumMonoTouchBindings
=========================

Xamarin.iOS bindings for the Leanplum iOS SDK.

Notes:
- Most of the binding is complete, but there are a few methods related to notifications that I haven't used, so they are not included.
- When building for simulator, add --registrar:static to additional mtouch arguments to avoid a "Failed to register System.Object" exceptions (as per https://bugzilla.xamarin.com/show_bug.cgi?id=23767).
