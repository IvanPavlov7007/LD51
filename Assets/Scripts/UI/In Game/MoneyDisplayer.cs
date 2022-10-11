using System.Collections;
using System.Collections.Generic;

public class MoneyDisplayer : PropertyDisplayer
{
    private void LateUpdate()
    {
        textMeshPro.text = gameManager.money.ToString();
    }
}
