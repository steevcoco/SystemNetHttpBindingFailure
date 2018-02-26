
# SystemNetHttpBindingFailure
Exhibits a bind failure due to the included Visual Studio System.Net.Http library.

#Update:

Please note that the fix I have found is not as described below. You CAN see the simple failure, which DOES appear to be a bug: this is a simple solution with no alterations, and it does not run ... My fix was to leave the VS library in place, but install Microsoft.Net.Http from NuGet --- and be sure not to include any Framework Net Extensions in the project --- I installed the NuGet package in the Wpf project; and it is running normally.

I believe this is a duplicate issue. I solved this by removing the Visual Studio lib/System.Net.Http.dll.

I have an extremely simple GitHub solution that exhibits the problem. This is Visual Studio 2017 Enterprise latest version.

A WPF 471 project references a NetStandard 2.0 library. The library has a class that uses `HttpMessageInvoker`. The Wpf app creates an HttpClient, and at runtime, the class library cannot be invoked because the binding has resolved the wrong assembly. Removing the Visual Studio dll has solved the problem.

The repo is here:

https://github.com/steevcoco/SystemNetHttpBindingFailure

You can note that the Wpf app does not reference System.Net or System.Net.Http ... It is somehow still able to construct the HttpClient. I thought it might be getting the NetStandard 2.0 reference transitively, but it appears that it is actually getting the Visual Studio library.

If you can't even build, it is because you do not have the Visual Studio Http lib. If you can, then the App is constructing the HttpClient from that VS lib.

Automatic binding redirects are on in both projects; and both use PackageRestore (which does not cure the issue).

To fix it, you can remove the Visual Studio System.Net.Http dll, and then in the Wpf project, use the Visual Studio "Add Reference" command to add the Framework Http library --- which is listed as 4.0.0.0. It will then run. You may not also that simply adding that framework reference does not work: I believe I then saw a duplicate reference error --- I had to remove the VS lib.

On my machine, the Visual Studio library is here:

C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\Microsoft\Microsoft.NET.Build.Extensions\net471\lib

For reference, you will get a runtime error like this:

System.MissingMethodException
  HResult=0x80131513
  Message=Method not found: 'System.Threading.Tasks.Task`1<ClassLibrary1.Envelope> ClassLibrary1.Envelope.PostAsync(System.Uri, System.Net.Http.HttpMessageInvoker, System.Threading.CancellationToken)'.
  Source=WpfApp2
  StackTrace:
   at WpfApp2.App.handleAppStartup(Object sender, StartupEventArgs e) in C:\Source\GitHub\Steevcoco\SystemNetHttpBindingFailure\Source\WpfApp2\App.xaml.cs:line 23
   at System.Windows.Application.OnStartup(StartupEventArgs e)
   at System.Windows.Application.<.ctor>b__1_0(Object unused)
   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)
   at System.Windows.Threading.DispatcherOperation.InvokeImpl()
   at System.Windows.Threading.DispatcherOperation.InvokeInSecurityContext(Object state)
   at MS.Internal.CulturePreservingExecutionContext.CallbackWrapper(Object obj)
   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
   at MS.Internal.CulturePreservingExecutionContext.Run(CulturePreservingExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Windows.Threading.DispatcherOperation.Invoke()
   at System.Windows.Threading.Dispatcher.ProcessQueue()
   at System.Windows.Threading.Dispatcher.WndProcHook(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
   at MS.Win32.HwndWrapper.WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
   at MS.Win32.HwndSubclass.DispatcherCallbackOperation(Object o)
   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)
   at System.Windows.Threading.Dispatcher.LegacyInvokeImpl(DispatcherPriority priority, TimeSpan timeout, Delegate method, Object args, Int32 numArgs)
   at MS.Win32.HwndSubclass.SubclassWndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam)
   at MS.Win32.UnsafeNativeMethods.DispatchMessage(MSG& msg)
   at System.Windows.Threading.Dispatcher.PushFrameImpl(DispatcherFrame frame)
   at System.Windows.Threading.Dispatcher.PushFrame(DispatcherFrame frame)
   at System.Windows.Application.RunDispatcher(Object ignore)
   at System.Windows.Application.RunInternal(Window window)
   at System.Windows.Application.Run(Window window)
   at System.Windows.Application.Run()
   at WpfApp2.App.Main()

Output:
   
'WpfApp2.exe' (CLR v4.0.30319: DefaultDomain): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_32\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll'. Cannot find or open the PDB file.
'WpfApp2.exe' (CLR v4.0.30319: DefaultDomain): Loaded 'C:\Source\GitHub\Steevcoco\SystemNetHttpBindingFailure\Source\WpfApp2\bin\Debug\WpfApp2.exe'. Symbols loaded.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\PresentationFramework\v4.0_4.0.0.0__31bf3856ad364e35\PresentationFramework.dll'. Symbols loaded.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\WindowsBase\v4.0_4.0.0.0__31bf3856ad364e35\WindowsBase.dll'. Symbols loaded.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Core\v4.0_4.0.0.0__b77a5c561934e089\System.Core.dll'. Symbols loaded.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System\v4.0_4.0.0.0__b77a5c561934e089\System.dll'. Symbols loaded.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_32\PresentationCore\v4.0_4.0.0.0__31bf3856ad364e35\PresentationCore.dll'. Cannot find or open the PDB file.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Xaml\v4.0_4.0.0.0__b77a5c561934e089\System.Xaml.dll'. Symbols loaded.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\PrivateAssemblies\Runtime\Microsoft.VisualStudio.Debugger.Runtime.dll'. 
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Configuration\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Configuration.dll'. Symbols loaded.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Xml\v4.0_4.0.0.0__b77a5c561934e089\System.Xml.dll'. Symbols loaded.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\Source\GitHub\Steevcoco\SystemNetHttpBindingFailure\Source\WpfApp2\bin\Debug\ClassLibrary1.dll'. Symbols loaded.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\netstandard\v4.0_2.0.0.0__cc7b13ffcd2ddd51\netstandard.dll'. Cannot find or open the PDB file.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\Source\GitHub\Steevcoco\SystemNetHttpBindingFailure\Source\WpfApp2\bin\Debug\System.Net.Http.dll'. Cannot find or open the PDB file.
'WpfApp2.exe' (CLR v4.0.30319: WpfApp2.exe): Loaded 'C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Net.Http\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Net.Http.dll'. Cannot find or open the PDB file.
Exception thrown: 'System.MissingMethodException' in PresentationFramework.dll
An unhandled exception of type 'System.MissingMethodException' occurred in PresentationFramework.dll
Method not found: 'System.Threading.Tasks.Task`1<ClassLibrary1.Envelope> ClassLibrary1.Envelope.PostAsync(System.Uri, System.Net.Http.HttpMessageInvoker, System.Threading.CancellationToken)'.
