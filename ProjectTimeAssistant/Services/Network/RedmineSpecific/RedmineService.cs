using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace ProjectTimeAssistant.Services.Network
{
    class RedmineService : INetworkDataService
    {
        public readonly Uri serverUrl = new Uri("http://onlab.m.redmine.org");

        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

        private async Task PostTAsync<T>(Uri uri, T t)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(t);
                HttpResponseMessage rpmsg = await client.PostAsync(uri, new StringContent(json, new UTF8Encoding(), "application/json"));

            }
        }

        public async Task<IssueContainer> GetIssuesAsync()
        {
            return await GetAsync<IssueContainer>(new Uri(serverUrl, $"issues.json"));
        }

        public async Task<ProjectContainer> GetProjectsAsync()
        {
            return await GetAsync<ProjectContainer>(new Uri(serverUrl, $"projects.json"));
        }

        public async Task<TimeEntriesContainer> GetTimeEntriesAsync()
        {
            return await GetAsync<TimeEntriesContainer>(new Uri(serverUrl, $"time_entries.json"));
        }

        public async Task PostTimeEntry(Time_Entry t)
        {
            t.key = "4f56fb8188c5f48811efe9a47b7ef50ad3443318";
            await PostTAsync<Time_Entry>(new Uri(serverUrl, $"time_entries.json"), t);
        }
    }
}
/*
{"issues":
[{"id":11,"project":{"id":5,"name":"Modellez\u00e9s"},"tracker":{"id":4,"name":"Tervez\u00e9s"},"status":{"id":1,"name":"\u00daj"},"priority":{"id":3,"name":"Magas"},"author":{"id":2,"name":"Redmine Admin"},"subject":"Oszt\u00e1lymodell kidolgoz\u00e1sa","description":"","start_date":"2017-02-21","done_ratio":30,"created_on":"2017-02-21T09:59:06Z","updated_on":"2017-02-21T15:20:13Z"},
 {"id":10,"project":{"id":5,"name":"Modellez\u00e9s"},"tracker":{"id":4,"name":"Tervez\u00e9s"},"status":{"id":1,"name":"\u00daj"},"priority":{"id":3,"name":"Magas"},"author":{"id":2,"name":"Redmine Admin"},"subject":"Adatmodell kidolgoz\u00e1sa","description":"","start_date":"2017-02-21","done_ratio":0,"created_on":"2017-02-21T09:58:57Z","updated_on":"2017-02-21T09:59:43Z"},
 {"id":9,"project":{"id":4,"name":"Redmine"},"tracker":{"id":2,"name":"Fejleszt\u00e9s"},"status":{"id":2,"name":"Folyamatban"},"priority":{"id":3,"name":"Magas"},"author":{"id":2,"name":"Redmine Admin"},"subject":"Feladatok hozz\u00e1ad\u00e1sa","description":"","start_date":"2017-02-21","done_ratio":70,"created_on":"2017-02-21T09:54:44Z","updated_on":"2017-02-21T10:05:22Z"},
 {"id":8,"project":{"id":2,"name":"Dokument\u00e1ci\u00f3"},"tracker":{"id":3,"name":"T\u00e1mogat\u00e1s"},"status":{"id":1,"name":"\u00daj"},"priority":{"id":2,"name":"Norm\u00e1l"},"author":{"id":2,"name":"Redmine Admin"},"subject":"Felhaszn\u00e1l\u00f3i dokument\u00e1ci\u00f3","description":"","start_date":"2017-02-21","done_ratio":0,"created_on":"2017-02-21T09:53:12Z","updated_on":"2017-02-21T09:53:12Z"},
 {"id":7,"project":{"id":2,"name":"Dokument\u00e1ci\u00f3"},"tracker":{"id":3,"name":"T\u00e1mogat\u00e1s"},"status":{"id":1,"name":"\u00daj"},"priority":{"id":1,"name":"Alacsony"},"author":{"id":2,"name":"Redmine Admin"},"subject":"Tesztel\u00e9si dokument\u00e1ci\u00f3","description":"","start_date":"2017-02-21","done_ratio":0,"created_on":"2017-02-21T09:52:49Z","updated_on":"2017-02-21T10:00:48Z"},
 {"id":6,"project":{"id":2,"name":"Dokument\u00e1ci\u00f3"},"tracker":{"id":2,"name":"Fejleszt\u00e9s"},"status":{"id":1,"name":"\u00daj"},"priority":{"id":2,"name":"Norm\u00e1l"},"author":{"id":2,"name":"Redmine Admin"},"subject":"Oszt\u00e1lymodell dokument\u00e1ci\u00f3","description":"","start_date":"2017-02-21","done_ratio":0,"created_on":"2017-02-21T09:52:30Z","updated_on":"2017-02-21T09:52:30Z"},
 {"id":5,"project":{"id":2,"name":"Dokument\u00e1ci\u00f3"},"tracker":{"id":2,"name":"Fejleszt\u00e9s"},"status":{"id":1,"name":"\u00daj"},"priority":{"id":2,"name":"Norm\u00e1l"},"author":{"id":2,"name":"Redmine Admin"},"subject":"Adatmoddell terv dokument\u00e1l\u00e1sa","description":"","start_date":"2017-02-21","done_ratio":0,"created_on":"2017-02-21T09:52:18Z","updated_on":"2017-02-21T09:52:18Z"},
 {"id":4,"project":{"id":3,"name":"GUI"},"tracker":{"id":2,"name":"Fejleszt\u00e9s"},"status":{"id":1,"name":"\u00daj"},"priority":{"id":2,"name":"Norm\u00e1l"},"author":{"id":2,"name":"Redmine Admin"},"subject":"UWP funkci\u00f3k kiismer\u00e9se","description":"","start_date":"2017-02-21","done_ratio":0,"created_on":"2017-02-21T09:51:09Z","updated_on":"2017-02-21T09:51:09Z"},
 {"id":3,"project":{"id":3,"name":"GUI"},"tracker":{"id":2,"name":"Fejleszt\u00e9s"},"status":{"id":1,"name":"\u00daj"},"priority":{"id":2,"name":"Norm\u00e1l"},"author":{"id":2,"name":"Redmine Admin"},"subject":"GUI protot\u00edpus","description":"UWP protot\u00edpus \u00edr\u00e1sa","start_date":"2017-02-21","done_ratio":0,"created_on":"2017-02-21T09:50:54Z","updated_on":"2017-02-21T09:50:54Z"},
 {"id":2,"project":{"id":3,"name":"GUI"},"tracker":{"id":2,"name":"Fejleszt\u00e9s"},"status":{"id":1,"name":"\u00daj"},"priority":{"id":2,"name":"Norm\u00e1l"},"author":{"id":2,"name":"Redmine Admin"},"subject":"GUI tervez\u00e9s","description":"Wireframe k\u00e9sz\u00edt\u00e9s","start_date":"2017-02-21","done_ratio":0,"created_on":"2017-02-21T09:50:25Z","updated_on":"2017-02-21T09:50:25Z"},
 {"id":1,"project":{"id":4,"name":"Redmine"},"tracker":{"id":2,"name":"Fejleszt\u00e9s"},"status":{"id":3,"name":"Megoldva"},"priority":{"id":2,"name":"Norm\u00e1l"},"author":{"id":2,"name":"Redmine Admin"},"subject":"Tesztprojekt \u00e9s alprojektek l\u00e9trehoz\u00e1sa","description":"","start_date":"2017-02-21","done_ratio":100,"created_on":"2017-02-21T09:47:10Z","updated_on":"2017-02-21T09:48:51Z"}],
 "total_count":11,"offset":0,"limit":25}
 */

/*
{"projects":
[{"id":1,"name":"\u00d6n\u00e1ll\u00f3 labor","identifier":"onallo-labor","description":"Felvett t\u00e9maki\u00edr\u00e1s szerinti projekt kivitelez\u00e9se","status":1,"created_on":"2017-02-21T09:44:14Z","updated_on":"2017-02-21T09:44:14Z"},
 {"id":2,"name":"Dokument\u00e1ci\u00f3","identifier":"dokumentacio","description":"","parent":{"id":1,"name":"\u00d6n\u00e1ll\u00f3 labor"},"status":1,"created_on":"2017-02-21T09:45:43Z","updated_on":"2017-02-21T09:45:43Z"},
 {"id":3,"name":"GUI","identifier":"gui","description":"","parent":{"id":1,"name":"\u00d6n\u00e1ll\u00f3 labor"},"status":1,"created_on":"2017-02-21T09:45:54Z","updated_on":"2017-02-21T09:45:54Z"},
 {"id":5,"name":"Modellez\u00e9s","identifier":"modellezes","description":"Program adat \u00e9s oszt\u00e1lymodellj\u00e9nek kidolgoz\u00e1sa","parent":{"id":1,"name":"\u00d6n\u00e1ll\u00f3 labor"},"status":1,"created_on":"2017-02-21T09:58:37Z","updated_on":"2017-02-21T09:58:37Z"},
 {"id":4,"name":"Redmine","identifier":"redmine","description":"","parent":{"id":1,"name":"\u00d6n\u00e1ll\u00f3 labor"},"status":1,"created_on":"2017-02-21T09:46:03Z","updated_on":"2017-02-21T09:46:03Z"}]
 ,"total_count":5,"offset":0,"limit":25} 
*/

/*
 {"time_entries":
[{"id":3,"project":{"id":5,"name":"Modellez\u00e9s"},"issue":{"id":11},"user":{"id":2,"name":"Redmine Admin"},"activity":{"id":8,"name":"Tervez\u00e9s"},"hours":2.0,"comments":"","spent_on":"2017-02-21","created_on":"2017-02-21T15:20:02Z","updated_on":"2017-02-21T15:20:02Z"},
 {"id":2,"project":{"id":4,"name":"Redmine"},"issue":{"id":9},"user":{"id":2,"name":"Redmine Admin"},"activity":{"id":9,"name":"Fejleszt\u00e9s"},"hours":1.0,"comments":"","spent_on":"2017-02-21","created_on":"2017-02-21T10:05:22Z","updated_on":"2017-02-21T10:05:22Z"},
 {"id":1,"project":{"id":4,"name":"Redmine"},"issue":{"id":1},"user":{"id":2,"name":"Redmine Admin"},"activity":{"id":9,"name":"Fejleszt\u00e9s"},"hours":1.0,"comments":"","spent_on":"2017-02-21","created_on":"2017-02-21T09:48:51Z","updated_on":"2017-02-21T09:48:51Z"}]
 ,"total_count":3,"offset":0,"limit":25}
 */
