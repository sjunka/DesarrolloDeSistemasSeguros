// Shim minimo de WindowsBase: solo lo que referencia ThreatModeling.Model.dll (TMT 7.3.51110).
using System.Collections.Generic;
namespace System.Windows.Threading
{
    public enum DispatcherPriority { Invalid = -1, Inactive = 0, SystemIdle, ApplicationIdle, ContextIdle, Background, Input, Loaded, Render, DataBind, Normal, Send }
    public sealed class Dispatcher { public object Invoke(DispatcherPriority priority, Delegate method) => method.DynamicInvoke(); }
    public abstract class DispatcherObject { public Dispatcher Dispatcher { get; } = new Dispatcher(); }
}
namespace System.Windows
{
    public delegate void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e);
    public delegate object CoerceValueCallback(DependencyObject d, object baseValue);
    public class PropertyMetadata
    {
        public PropertyMetadata() { }
        public PropertyMetadata(object defaultValue) { DefaultValue = defaultValue; }
        public PropertyMetadata(PropertyChangedCallback propertyChangedCallback) { PropertyChangedCallback = propertyChangedCallback; }
        public PropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback) : this(defaultValue) { PropertyChangedCallback = propertyChangedCallback; }
        public PropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback) : this(defaultValue, propertyChangedCallback) { CoerceValueCallback = coerceValueCallback; }
        public object DefaultValue { get; set; }
        public PropertyChangedCallback PropertyChangedCallback { get; set; }
        public CoerceValueCallback CoerceValueCallback { get; set; }
    }
    public sealed class DependencyProperty
    {
        internal DependencyProperty(string name, Type propertyType, Type ownerType, PropertyMetadata metadata, bool readOnly)
        { Name = name; PropertyType = propertyType; OwnerType = ownerType; DefaultMetadata = metadata ?? new PropertyMetadata(); ReadOnly = readOnly; }
        public string Name { get; }
        public Type PropertyType { get; }
        public Type OwnerType { get; }
        public PropertyMetadata DefaultMetadata { get; }
        public bool ReadOnly { get; }
        internal object Default => DefaultMetadata.DefaultValue ?? (PropertyType.IsValueType ? Activator.CreateInstance(PropertyType) : null);
        public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata) => new(name, propertyType, ownerType, typeMetadata, false);
        public static DependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata) => new(new(name, propertyType, ownerType, typeMetadata, true));
    }
    public sealed class DependencyPropertyKey
    {
        internal DependencyPropertyKey(DependencyProperty dp) { DependencyProperty = dp; }
        public DependencyProperty DependencyProperty { get; }
    }
    public readonly struct DependencyPropertyChangedEventArgs
    {
        public DependencyPropertyChangedEventArgs(DependencyProperty property, object oldValue, object newValue) { Property = property; OldValue = oldValue; NewValue = newValue; }
        public DependencyProperty Property { get; }
        public object OldValue { get; }
        public object NewValue { get; }
    }
    // Semantica WPF basica: valor local o default, coercion y callback solo si el valor efectivo cambia.
    public class DependencyObject : Threading.DispatcherObject
    {
        private readonly Dictionary<DependencyProperty, object> _local = new();
        public object GetValue(DependencyProperty dp) => _local.TryGetValue(dp, out var v) ? v : dp.Default;
        public void SetValue(DependencyProperty dp, object value)
        {
            if (dp.ReadOnly) throw new InvalidOperationException($"'{dp.Name}' es de solo lectura.");
            Apply(dp, value, true);
        }
        public void SetValue(DependencyPropertyKey key, object value) => Apply(key.DependencyProperty, value, true);
        public void ClearValue(DependencyProperty dp)
        {
            if (dp.ReadOnly) throw new InvalidOperationException($"'{dp.Name}' es de solo lectura.");
            Apply(dp, dp.Default, false);
        }
        public void ClearValue(DependencyPropertyKey key) => Apply(key.DependencyProperty, key.DependencyProperty.Default, false);
        private void Apply(DependencyProperty dp, object baseValue, bool local)
        {
            var old = GetValue(dp);
            var coerce = dp.DefaultMetadata.CoerceValueCallback;
            var value = coerce != null ? coerce(this, baseValue) : baseValue;
            if (local || !Equals(value, dp.Default)) _local[dp] = value; else _local.Remove(dp);
            if (!Equals(old, value)) dp.DefaultMetadata.PropertyChangedCallback?.Invoke(this, new DependencyPropertyChangedEventArgs(dp, old, value));
        }
    }
    public struct Point { public Point(double x, double y) { X = x; Y = y; } public double X { get; set; } public double Y { get; set; } }
    public struct Vector { public Vector(double x, double y) { X = x; Y = y; } public double X { get; set; } public double Y { get; set; } }
    public struct Rect
    {
        public Rect(double x, double y, double width, double height) { X = x; Y = y; Width = width; Height = height; }
        public double X { get; set; } public double Y { get; set; } public double Width { get; set; } public double Height { get; set; }
        public double Left => X; public double Top => Y;
    }
}
