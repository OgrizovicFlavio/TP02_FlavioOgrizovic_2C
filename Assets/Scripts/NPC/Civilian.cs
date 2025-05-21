
public class Civilian : NPCController
{
    protected override void AddScore()
    {
        if (EnemyKillManager.Instance != null)
            EnemyKillManager.Instance.RegisterKill(stats.killValue);
    }
}