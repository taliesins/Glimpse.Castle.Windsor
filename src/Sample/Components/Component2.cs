namespace Sample.Components
{
    public class Component2 : IComponent2
    {
        private IComponent1 component1;

        public Component2(IComponent1 component1)
        {
            this.component1 = component1;
        }
    }
}