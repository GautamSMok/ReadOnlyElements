# Create Readonly list in C#

The framework will help to create readonly list. It also converts normal list of values to be immutable. It forces to define reaonly classes if you need immutable list of
readonly objects.

### Examples

Following examples show how we can use this framework to create readonly list and readonly values.

```
var list = ReadOnlyElement<float>.GetReadOnlyList(2.3f, 2.0f);
list.Select(e => e.Value).ToList().ForEach(Console.WriteLine);
```

```
var list = ReadOnlyElement<string>.GetReadOnlyList("ABC", "pqr", "XYZ");
list[0]=null;//ERROR: 'Property or indexer 'ReadOnlyCollection<ReadOnlyElement<string>>.this[int]' cannot be assigned to -- it is read only
list.Select(e => e.Value).ToList().ForEach(Console.WriteLine);
```

### How to install

Install it from Nuget

Or download 'ReadOnlyElement.dll' and add it as reference in your project and start using
