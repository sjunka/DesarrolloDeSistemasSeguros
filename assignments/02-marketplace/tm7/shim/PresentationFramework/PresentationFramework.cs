// Shim minimo de PresentationFramework para TMT 7.3.51110. MessageBox se imprime en stderr (lo que el profe veria como dialogo).
using System.Globalization;
using System.IO;
namespace System.Windows
{
    public enum MessageBoxButton { OK = 0, OKCancel = 1, YesNoCancel = 3, YesNo = 4 }
    public enum MessageBoxImage { None = 0, Hand = 16, Question = 32, Exclamation = 48, Asterisk = 64, Stop = 16, Error = 16, Warning = 48, Information = 64 }
    public enum MessageBoxResult { None = 0, OK = 1, Cancel = 2, Yes = 6, No = 7 }
    [Flags] public enum MessageBoxOptions { None = 0, DefaultDesktopOnly = 0x20000, RightAlign = 0x80000, RtlReading = 0x100000, ServiceNotification = 0x200000 }
    public static class MessageBox
    {
        public static MessageBoxResult Show(string messageBoxText) => Log(messageBoxText, "");
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button) => Log(messageBoxText, caption);
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options) => Log(messageBoxText, caption);
        private static MessageBoxResult Log(string text, string caption) { Console.Error.WriteLine($"[MessageBox] {caption}: {text}"); return MessageBoxResult.OK; }
    }
    public class Application : Threading.DispatcherObject
    {
        public static Application Current => null;
        public static Resources.StreamResourceInfo GetResourceStream(Uri uriResource) => null;
    }
    public static class SystemParameters { public static bool HighContrast => false; }
}
namespace System.Windows.Resources { public class StreamResourceInfo { public Stream Stream { get; } } }
namespace System.Windows.Controls
{
    public class ValidationResult
    {
        public ValidationResult(bool isValid, object errorContent) { IsValid = isValid; ErrorContent = errorContent; }
        public bool IsValid { get; } public object ErrorContent { get; }
    }
    public abstract class ValidationRule
    {
        protected ValidationRule() { }
        public abstract ValidationResult Validate(object value, CultureInfo cultureInfo);
    }
    public class ScrollViewer
    {
        public double HorizontalOffset => 0; public double VerticalOffset => 0;
        public double ViewportHeight => 0; public double ViewportWidth => 0;
        public void InvalidateScrollInfo() { }
    }
}
