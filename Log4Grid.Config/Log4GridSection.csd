<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="285d0f08-41d5-4438-aaa7-a22d7f1ea59a" namespace="Log4Grid.Config" xmlSchemaNamespace="urn:Log4Grid.Config" assemblyName="Log4Grid.Config" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="Log4GridSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="log4GridSection">
      <elementProperties>
        <elementProperty name="Management" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="management" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/285d0f08-41d5-4438-aaa7-a22d7f1ea59a/ManagementHandler" />
          </type>
        </elementProperty>
        <elementProperty name="LogStore" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="logStore" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/285d0f08-41d5-4438-aaa7-a22d7f1ea59a/ManagementHandler" />
          </type>
        </elementProperty>
        <elementProperty name="LogSearch" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="logSearch" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/285d0f08-41d5-4438-aaa7-a22d7f1ea59a/ManagementHandler" />
          </type>
        </elementProperty>
        <elementProperty name="User" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="user" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/285d0f08-41d5-4438-aaa7-a22d7f1ea59a/ManagementHandler" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="ManagementHandler">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/285d0f08-41d5-4438-aaa7-a22d7f1ea59a/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Type" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="type" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/285d0f08-41d5-4438-aaa7-a22d7f1ea59a/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Properties" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="properties" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/285d0f08-41d5-4438-aaa7-a22d7f1ea59a/PropertyCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="Property">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/285d0f08-41d5-4438-aaa7-a22d7f1ea59a/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Value" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/285d0f08-41d5-4438-aaa7-a22d7f1ea59a/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="PropertyCollection" xmlItemName="property" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/285d0f08-41d5-4438-aaa7-a22d7f1ea59a/Property" />
      </itemType>
    </configurationElementCollection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>