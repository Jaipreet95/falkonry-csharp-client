using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace falkonry_csharp_client.helper.models
{
    public class AssessmentRequest
    {

        public string name
        {
            get;
            set;
        }
        public List<string> inputList
        {
            get;
            set;

        }
        public List<string> aprioriConditionList
        {
            get;
            set;
        }
    }
}