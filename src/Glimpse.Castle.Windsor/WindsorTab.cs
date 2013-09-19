using System.Linq;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Extensions;
using Glimpse.Core.Tab.Assist;

namespace Glimpse.Castle.Windsor
{
    public class WindsorTab : TabBase, ITabSetup, IKey, ITabLayout
    {
        private static readonly object layout = TabLayout.Create()
          .Row(r =>
          {
              r.Cell(0);
              r.Cell(1);
              r.Cell(2);
              r.Cell(3);
              r.Cell(4);
              r.Cell(5).Suffix(" ms").AlignRight().Prefix("T+ ").Class("mono");
              r.Cell(6).Suffix(" ms").AlignRight().Class("mono");
          }).Build();

        public override object GetData(ITabContext context)
        {
            return context.GetMessages<ComponentModelMessage>().ToList();
        }

        public override string Name
        {
            get { return "Castle Windsor"; }
        }

        public void Setup(ITabSetupContext context)
        {
            context.PersistMessages<ComponentModelMessage>();
        }

        public string Key
        {
            get { return "glimpse_windsor"; }
        }

        public object GetLayout()
        {
            return layout;
        }

       
    }
}