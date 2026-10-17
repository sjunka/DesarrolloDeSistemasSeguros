// Shim minimo de PresentationCore para TMT 7.3.51110 (sin UI: portapapeles e imagenes no hacen nada).
using System.Collections.Generic;
using System.IO;
namespace System.Windows
{
    public class UIPropertyMetadata : PropertyMetadata
    {
        public UIPropertyMetadata() { }
        public UIPropertyMetadata(object defaultValue) : base(defaultValue) { }
        public UIPropertyMetadata(PropertyChangedCallback propertyChangedCallback) : base(propertyChangedCallback) { }
        public UIPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback) : base(defaultValue, propertyChangedCallback) { }
        public UIPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback) : base(defaultValue, propertyChangedCallback, coerceValueCallback) { }
    }
    public class UIElement : DependencyObject { public void UpdateLayout() { } }
    public interface IDataObject
    {
        object GetData(string format);
        bool GetDataPresent(string format);
        void SetData(string format, object data);
        void SetData(string format, object data, bool autoConvert);
    }
    public sealed class DataObject : IDataObject
    {
        private readonly Dictionary<string, object> _data = new();
        public object GetData(string format) => _data.TryGetValue(format, out var v) ? v : null;
        public bool GetDataPresent(string format) => _data.ContainsKey(format);
        public void SetData(string format, object data) => _data[format] = data;
        public void SetData(string format, object data, bool autoConvert) => _data[format] = data;
        public void SetText(string textData) => _data["Text"] = textData;
    }
    public sealed class DataFormat { public DataFormat(string name) { Name = name; } public string Name { get; } }
    public static class DataFormats
    {
        public static readonly string Html = "HTML Format";
        public static DataFormat GetDataFormat(string format) => new(format);
    }
    public static class Clipboard
    {
        public static bool ContainsData(string format) => false;
        public static IDataObject GetDataObject() => null;
        public static void SetDataObject(object data) { }
        public static void SetDataObject(object data, bool copy) { }
    }
}
namespace System.Windows.Media.Imaging
{
    public sealed class BitmapImage
    {
        public void BeginInit() { }
        public void EndInit() { }
        public Stream StreamSource { get; set; }
    }
}
