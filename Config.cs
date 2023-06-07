using System;
using System.Collections.Generic;
using System.Linq;
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
	public class Config
	{
	    public Config()
	    {
	        _sections = new Dictionary<int, Section>();
	    }
	
	    [JsonProperty("sections")]
	    [DataMember]
	    private Dictionary<int, Section> _sections;
	
	    public Section AddSection(string name)
	    {
	        if(_sections.Values.Any(s => s.Name == name))
	        {
	            throw new ArgumentException("A Config Section with this name already exists.");
	        }
	        _sections.Add(_sections.Count, new Section(name));
	        return _sections[_sections.Count - 1];
	    } 
	
	    public Section AddSection(Section section) {
	        if(_sections.Values.Any(s => s.Name == section.Name))
	        {
	            throw new ArgumentException("A Config Section with this name already exists.");
	        }
	        _sections.Add(_sections.Count, section);
	        return _sections[_sections.Count - 1];
	    }
	
	    public Section GetSection(string Name)
	    {
	        if (_sections.Values.Any(s => s.Name == Name))
	        {
	            return _sections.Values.First(s => s.Name == Name);
	        }
	        else throw new ArgumentOutOfRangeException("Config Section with name: " + Name + " does not exist.");
	    }
	
	    private List<KeyValuePair<int, Section>> sectionsWithSetting ( string SettingName )
	    {
	        return _sections.Where(k => k.Value.Settings.Any(s => s.Name == SettingName)).ToList();
	    }
	
	    public Setting GetSetting(string Name)
	    {
	        var sectionsKV = sectionsWithSetting(Name);
	
	        if (sectionsKV.Count() > 0)
	        {
	            var FirstSection = sectionsKV.MinBy(k => k.Key);
	
	            return FirstSection.Value.Settings.First(s => s.Name == Name);
	        }
	
	        else throw new ArgumentOutOfRangeException("There is no setting named " + Name + " found in the configuration.");
	    }
	
	    public Setting GetSetting(string Name, string SectionName)
	    {
	        var sections = _sections.Where(s => s.Value.Name == SectionName).ToList();
	
	        if (sections.Count > 0)
	        {
	            if (sections[0].Value.Settings.Any(s => s.Name == Name))
	            {
	                return sections[0].Value.Settings.First(s => s.Name == Name);
	            }
	            else throw new ArgumentOutOfRangeException("Setting " + Name + " does not exist in section " + SectionName);
	        }
	
	        else throw new ArgumentOutOfRangeException("Section " + SectionName + " does not exist in config.");
	    }
	
	    public Setting AddSetting(Section section, string name, string value)
	    {
	        if (section.Settings.Any(s => s.Name == name))
	        {
	            section.Settings.First(s => s.Name == name).Value = value;
	        }
	        else
	        {
	            section.Settings.Add(new Setting(name, value));
	        }
	
	        return section.Settings.First(s => s.Name == name);
	    }
	
	    public Setting AddSetting(Section section, string name, string assetName, string assetValue)
	    {
	        section.Settings.Add(new Setting(name, assetName, new AssetSetting(assetValue)));
	
	        return section.Settings.First(s => s.Name == name);
	    }
	
	    public string ToJson() { return JsonConvert.SerializeObject(this); }
	}
}