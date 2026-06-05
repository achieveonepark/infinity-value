---
id: json
title: JSON
sidebar_position: 3
---

# JSON

安装 Unity 的 `com.unity.nuget.newtonsoft-json` 包后，`InfinityValue` 会自动应用 `InfinityValueConverter`。

转换器将值以字符串形式存储：

```json
{
  "gold": "5.30B"
}
```

## 示例

```csharp
using Achieve.InfinityValue;
using Newtonsoft.Json;

public sealed class SaveData
{
    public InfinityValue Gold;
}

var units = new InfinityValueUnitNames(new[] { "", "K", "M", "B" });
var save = new SaveData
{
    Gold = new InfinityValue("5B", units)
};

string json = JsonConvert.SerializeObject(save);
SaveData loaded = JsonConvert.DeserializeObject<SaveData>(json);
```

默认转换器使用默认单位名称重建值。如果存档文件使用了自定义单位名称，请在保存游戏数据时一并保存足够的内容元数据，以便加载时能选择正确的 `InfinityValueUnitNames`。
