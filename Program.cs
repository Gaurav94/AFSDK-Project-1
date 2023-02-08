using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.EventFrame;
using OSIsoft.AF.Search;
using OSIsoft.AF.Time;

namespace DECCPI_Heartbeat_Eventframes
{
    internal class Program
    {
      [Obsolete]
        static void Main(string[] args)
        {

            PISystems piSystems = new PISystems();
            PISystem piSystem = piSystems["XXXXX"];
            AFDatabase afDatabase = piSystem.Databases["XXXXXX"];
            var timerange = new AFTimeRange("1-jun-2021", "1-feb-2023", CultureInfo.InvariantCulture);

            List<AFSearchToken> tokenList = new List<AFSearchToken>();
            tokenList.Add(new AFSearchToken(AFSearchFilter.Template, AFSearchOperator.Equal, "STS-Heartbeat"));
             tokenList.Add(new AFSearchToken(AFSearchFilter.Start, AFSearchOperator.GreaterThan, "1-aug-2021"));
             tokenList.Add(new AFSearchToken(AFSearchFilter.End, AFSearchOperator.LessThanOrEqual, "1-nov-2021"));
            var tokenSearch = new AFEventFrameSearch(afDatabase, "Template Search", tokenList);




            // var startTime = "1-jan-2023" as AFTime?;
            // var endTime =  as AFTime?;


            var results = tokenSearch.FindEventFrames(0, true, 100000);
            

           // var counter = 0;
            foreach (var item in results)
            {
                
                Console.WriteLine($"Event {item.Name}, time: {item.StartTime} - {item.EndTime} Duration: {item.Duration}");
                item.Delete();
                //Console.ReadKey();
                //      counter++;
                //      if (counter > 9) break;
            }
            afDatabase.CheckIn(AFCheckedOutMode.ObjectsCheckedOutThisSession);






       /*     const int pageSize = 1000;
            int startIndex = 0;
            int returnLimit = 100000;
            do
            {
                AFElementTemplate efTemplate = afDatabase.ElementTemplates["BasicEventFrameTemplate"];

                // Get event frames that started the past two days.
                AFNamedCollectionList<AFEventFrame> eventFrames = AFEventFrame.FindEventFrames(
                    database: afDatabase,
                    searchRoot: null,
                    startTime: "1-jan-2023",
                    startIndex: startIndex,
                    maxCount: pageSize,
                    searchMode: AFEventFrameSearchMode.ForwardFromStartTime,
                    nameFilter: "*heartbeat*",
                    referencedElementNameFilter: "System",
                    elemCategory: null,
                    elemTemplate: efTemplate,
                    searchFullHierarchy: true);

                foreach (AFEventFrame ef in eventFrames)
                {
                    //Note: We should make a bulk call on the attribute values via AFAttributeList if we had many event frames.
                    Console.WriteLine("Name: {0}, Start: {1}, End: {2}",
                        ef.Name, ef.StartTime, ef.EndTime);
                        //,ef.Attributes["Maximum temperature"].GetValue());
                }

                startIndex += pageSize;

            } while (startIndex < returnLimit); */
        }

    }
   }
