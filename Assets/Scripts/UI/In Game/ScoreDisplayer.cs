using System.Collections;
using System.Collections.Generic;

public class ScoreDisplayer : PropertyDisplayer
{
    private void LateUpdate()
    {
        textMeshPro.text = gameManager.score.ToString();
    }
}
