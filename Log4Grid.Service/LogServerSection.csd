<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="b5afb505-ca40-4720-aa16-540ceec24c2a" namespace="Log4Grid.Service" xmlSchemaNamespace="urn:Log4Grid.Service" assemblyName="Log4Grid.Service" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="LogServerSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="logServerSection">
      <attributeProperties>
        <attributeProperty name="Host" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="host" isReadOnly="false" defaultValue="&quot;127.0.0.1&quot;">
          <type>
            <externalTypeMoniker name="/b5afb505-ca40-4720-aa16-540ceec24c2a/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false" defaultValue="10343">
          <type>
            <externalTypeMoniker name="/b5afb505-ca40-4720-aa16-540ceec24c2a/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="WorkThreads" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="workThreads" isReadOnly="false" defaultValue="1">
          <type>
            <externalTypeMoniker name="/b5afb505-ca40-4720-aa16-540ceec24c2a/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationSection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>