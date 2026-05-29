---
id: json
title: JSON
sidebar_position: 3
---

# JSON

Unity의 `com.unity.nuget.newtonsoft-json` 패키지가 설치되어 있으면 `InfinityValue`에 `InfinityValueConverter`가 자동으로 적용됩니다.

컨버터는 값을 문자열로 저장합니다:

```json
{
  "gold": "5.30B"
}
```

## 예제

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

기본 컨버터는 기본 단위 이름으로 값을 복원합니다. 저장 파일이 커스텀 단위 이름을 사용한다면, 게임플레이 데이터 로드 시 올바른 `InfinityValueUnitNames`를 선택할 수 있도록 충분한 메타데이터를 함께 저장하세요.
