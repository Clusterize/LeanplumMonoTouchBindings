using ObjCRuntime;

[assembly: LinkWith ("libLeanplum.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.Arm64, ForceLoad = true, Frameworks = "CFNetwork SystemConfiguration Security", WeakFrameworks = "AdSupport StoreKit CoreLocation")]