#tool nuget:?package=XamarinComponent&version=1.1.0.49

#addin nuget:?package=Cake.Xamarin.Build&version=1.0.14.0
#addin nuget:?package=Cake.Xamarin

BuildSpec buildSpec = null;


var TARGET = Argument ("t", Argument ("target", "Default"));

buildSpec = new BuildSpec () {

	Libs = new ISolutionBuilder [] { 
		new DefaultSolutionBuilder {
			SolutionPath = "src/SignaturePad.Mac.sln",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Android/bin/Release/SignaturePad.dll",
					ToDirectory = "output/android",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.iOS/bin/unified/Release/SignaturePad.dll",
					ToDirectory = "output/ios-unified",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Forms/bin/Release/SignaturePad.Forms.dll",
					ToDirectory = "output/pcl",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Forms.Droid/bin/Release/SignaturePad.Forms.dll",
					ToDirectory = "output/android",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Forms.iOS/bin/Release/SignaturePad.Forms.dll",
					ToDirectory = "output/ios-unified",
				},
			}
		},
		new WpSolutionBuilder {
			SolutionPath = "src/SignaturePad.sln",
			BuildsOn = BuildPlatforms.Windows,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Android/bin/Release/SignaturePad.dll",
					ToDirectory = "output/android",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.iOS/bin/unified/Release/SignaturePad.dll",
					ToDirectory = "output/ios-unified",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Forms/bin/Release/SignaturePad.Forms.dll",
					ToDirectory = "output/pcl",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Forms.Droid/bin/Release/SignaturePad.Forms.dll",
					ToDirectory = "output/android",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Forms.iOS/bin/Release/SignaturePad.Forms.dll",
					ToDirectory = "output/ios-unified",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.WP8/bin/Release/SignaturePad.dll",
					ToDirectory = "output/wp8",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Forms.WindowsPhone/bin/Release/SignaturePad.Forms.dll",
					ToDirectory = "output/wp8",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.UWP/bin/Release/SignaturePad.dll",
					ToDirectory = "output/uwp",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Forms.UWP/bin/Release/SignaturePad.Forms.dll",
					ToDirectory = "output/uwp",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Windows81/bin/Release/SignaturePad.dll",
					ToDirectory = "output/win",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Forms.Windows81/bin/Release/SignaturePad.Forms.dll",
					ToDirectory = "output/win",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.WindowsPhone81/bin/Release/SignaturePad.dll",
					ToDirectory = "output/wpa",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.Forms.WindowsPhone81/bin/Release/SignaturePad.Forms.dll",
					ToDirectory = "output/wpa",
				},
				new OutputFileCopy {
					FromFile = "./src/SignaturePad.WindowsRuntime81/bin/Release/SignaturePad.dll",
					ToDirectory = "output/winrt",
				},
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/Sample.Android/Sample.Android.sln", BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows },
		new IOSSolutionBuilder { SolutionPath = "./samples/Sample.iOS/Sample.iOS.sln", BuildsOn = BuildPlatforms.Mac },
		new IOSSolutionBuilder { SolutionPath = "./samples/Sample.Forms/Sample.Forms.Mac.sln", BuildsOn = BuildPlatforms.Mac },
		new WpSolutionBuilder { SolutionPath = "./samples/Sample.WP8/Sample.WP8.sln", BuildsOn = BuildPlatforms.Windows }, 
		new WpSolutionBuilder { SolutionPath = "./samples/Sample.UWP/Sample.UWP.sln", BuildsOn = BuildPlatforms.Windows }, 
		new WpSolutionBuilder { SolutionPath = "./samples/Sample.Windows81/Sample.Windows81.sln", BuildsOn = BuildPlatforms.Windows }, 
		new WpSolutionBuilder { SolutionPath = "./samples/Sample.WindowsPhone81/Sample.WindowsPhone81.sln", BuildsOn = BuildPlatforms.Windows }, 
		new WpSolutionBuilder { SolutionPath = "./samples/Sample.WindowsRuntime81/Sample.WindowsRuntime81.sln", BuildsOn = BuildPlatforms.Windows }, 
		new WpSolutionBuilder { SolutionPath = "./samples/Sample.Forms/Sample.Forms.Win.sln", BuildsOn = BuildPlatforms.Windows }, 
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Controls.SignaturePad.nuspec", BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Controls.SignaturePad.Forms.nuspec", BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows },
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

Task ("CI")
	.IsDependentOn ("libs")
	.IsDependentOn ("nuget")
	.IsDependentOn ("component")
	.IsDependentOn ("samples");

RunTarget (TARGET);
