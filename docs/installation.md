# 설치 가이드

## 요구 사항

- Unity **2022.3** 이상
- .NET Standard 2.1+

---

## UPM으로 설치하기

1. **Package Manager** 창을 엽니다 (`Window → Package Manager`).
2. 좌측 상단의 **`+`** 버튼을 클릭합니다.
3. **Add package from git URL...** 을 선택합니다.
4. 아래 URL을 입력하고 **Add**를 클릭합니다.

```
https://github.com/achieveonepark/InfinityValue.git#1.1.0
```

---

## manifest.json으로 설치하기

프로젝트의 `Packages/manifest.json` 파일을 열고 `dependencies` 항목에 다음을 추가합니다.

```json
{
  "dependencies": {
    "com.achieve.infinity-value": "https://github.com/achieveonepark/InfinityValue.git#1.1.0"
  }
}
```

---

## 선택 사항: Newtonsoft.Json 지원

**Newtonsoft.Json** (`com.unity.nuget.newtonsoft-json` 등)이 프로젝트에 설치되어 있다면, 스크립팅 정의 심볼을 추가하여 JSON 직렬화를 활성화할 수 있습니다.

1. **Edit → Project Settings → Player**로 이동합니다.
2. **Scripting Define Symbols** 항목에 아래 심볼을 추가합니다.

```
USE_NEWTONSOFT_JSON
```

자세한 사용법은 [JSON 직렬화](/advanced/json) 페이지를 참고하세요.

---

## 설치 확인

아래 코드를 아무 MonoBehaviour에 추가해서 패키지가 정상 동작하는지 확인하세요.

```csharp
using Achieve.InfinityValue;
using UnityEngine;

public class InstallCheck : MonoBehaviour
{
    void Start()
    {
        InfinityValue v = 1_500_000L;
        Debug.Log(v); // 출력 예: "1.50A"
    }
}
```

> **다음:** [빠른 시작 →](/getting-started)
