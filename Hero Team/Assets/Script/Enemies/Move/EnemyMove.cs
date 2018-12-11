public class EnemyMove : BaseEnemyMove
{
    void Update()
    {
        transform.Translate(0, -0.01f, 0);
    }
}