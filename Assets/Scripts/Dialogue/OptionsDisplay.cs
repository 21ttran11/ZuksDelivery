using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if USE_TMP
using TMPro;
#else
using TextMeshProUGUI = TMPShim;
#endif

namespace Yarn.Unity
{
    public class OptionsBubbleView : DialogueViewBase
    {
        [SerializeField] OptionView optionViewPrefab;
    }
}