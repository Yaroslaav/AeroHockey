using SFML.Graphics;

public abstract class GameObject
{
    public Transformable transformable { get; set; }

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        
    }
    public virtual void OnDestroy()
    {
        
    }
}