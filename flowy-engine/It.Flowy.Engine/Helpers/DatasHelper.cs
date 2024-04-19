using It.Flowy.Engine.Models.Common;
using It.Flowy.Engine.Models.Modelling;

namespace It.Flowy.Engine.Helpers;

public static class DatasHelper {

  public static T? FindValue<T>(this ICollection<ActivityData> datas, string name) {
    var find = datas.FirstOrDefault(d => d.Name != null && d.Name == name);
    return Convert<T>(find?.Value);
  }

  public static T? FindValue<T>(this ICollection<NodeData> datas, string name) {
    var find = datas.FirstOrDefault(d => d.Name != null && d.Name == name);
    return Convert<T>(find?.Value);
  }

  private static T? Convert<T>(string? value) {
    if (value == null) { return default; }
    return (T)(object)(value);
  }

  public static bool CheckValue(this ICollection<NodeData>? datas, string name, string value, bool ignoreCas = false) {
    var find = datas?.FirstOrDefault(d => d.Name != null && d.Name == name);
    return Check(find, value, ignoreCas);
  }
  public static bool CheckValue(this ICollection<ActivityData>? datas, string name, string value, bool ignoreCas = false) {
    var find = datas?.FirstOrDefault(d => d.Name != null && d.Name == name);
    return Check(find, value, ignoreCas);
  }

  private static bool Check(Data? find, string value, bool ignoreCas = false){
    if (find == null) { return false; }
    if (find.Value == null) { return false; }
    if (ignoreCas && find.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase)){ return true;}
    if (find.Value == value) { return true; }
    return false;
  }
}

public static class ConfigActivity {
  public static readonly string PROCESSING_ACTIVITY_ISAUTOMATIC = "Processing.Activity.IsAutomatic";
  public static readonly string PROCESSING_ACTIVITY_NAME = "Processing.Activity.Name";
}