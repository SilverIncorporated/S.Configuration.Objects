using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.Serialization;

// NOTICE: The Coded Automations feature is available as a preview feature and APIs may be subject to change. 
//         No warranty or technical support is provided for this preview feature.
//         Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// Please delete these comments after you've read and acknowledged them. For more information, please visit the documentation over at https://docs.uipath.com/studio/lang-en/v2023.4/docs/coded-automations.

namespace S.Configuration.Objects
{
	

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[DataContract]
	public class Section
	{
	    public Section(string name)
	    {
	        Settings = new List<Setting>();
	
	        Name = name;
	    }
	
	    [JsonProperty(propertyName: "name")]
	    [DataMember]
	    public string Name { get; set; }
	
	    [JsonProperty(propertyName: "settings")]
	    [DataMember]
	    public List<Setting> Settings { get; set; }
	
	    public string ToJson() { return JsonConvert.SerializeObject(this); }
	}
	}