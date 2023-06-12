namespace CodeBase
{
    public enum TypeMode
    {
        Empty = 0,
        FirstPerson = 1,
        Orbital = 2
    };
    
    public interface ICameraMode
    {
        void ChangeMode(TypeMode mode);
    }
}