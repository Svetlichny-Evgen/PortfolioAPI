using MongoDB.Bson.Serialization.Attributes;

namespace TeamPortfolio.Models
{
    public class Skills
    {
        [BsonElement("programming_languages")]
        public List<string> ProgrammingLanguages { get; set; }

        [BsonElement("technologies")]
        public List<string> Technologies { get; set; }

        [BsonElement("frameworks_and_libraries")]
        public List<string> FrameworksAndLibraries { get; set; }

        [BsonElement("databases")]
        public List<string> Databases { get; set; }

        [BsonElement("frontend")]
        public List<string> Frontend { get; set; }

        [BsonElement("developer_environments")]
        public List<string> DeveloperEnvironments { get; set; }

        [BsonElement("other")]
        public List<string> Other { get; set; }

        [BsonElement("operating_systems_and_virtualization")]
        public List<string> OperatingSystemsAndVirtualization { get; set; }

        [BsonElement("networking_technologies_and_hardware")]
        public List<string> NetworkingTechnologiesAndHardware { get; set; }

        [BsonElement("containerization_and_orchestration")]
        public List<string> ContainerizationAndOrchestration { get; set; }

        [BsonElement("monitoring_systems")]
        public List<string> MonitoringSystems { get; set; }

        [BsonElement("version_control_systems")]
        public List<string> VersionControlSystems { get; set; }

        [BsonElement("web_servers")]
        public List<string> WebServers { get; set; }

        [BsonElement("message_broker")]
        public List<string> MessageBroker { get; set; }
    }
}
