using System;

namespace WSApi.Client.Models.Entities.Contacts;

public class VCard
{
    public string FullName { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PhoneType { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;

    // Constructor from vCard string
    public VCard(string vcardString)
    {
        ParseVCard(vcardString);
    }

    // Constructor from individual fields
    public VCard(string fullName, string familyName, string givenName, string phone, string phoneType, string organization)
    {
        FullName = fullName;
        FamilyName = familyName;
        GivenName = givenName;
        Phone = phone;
        PhoneType = phoneType;
        Organization = organization;
    }

    private void ParseVCard(string vcardString)
    {
        var lines = vcardString.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            if (line.StartsWith("FN:"))
                FullName = line[3..];
            else if (line.StartsWith("N:"))
            {
                var parts = line[2..].Split(';');
                if (parts.Length > 0) FamilyName = parts[0];
                if (parts.Length > 1) GivenName = parts[1];
            }
            else if (line.StartsWith("TEL"))
            {
                var colonIndex = line.IndexOf(':');
                if (colonIndex <= 0)
                    continue;

                Phone = line[(colonIndex + 1)..];
                var typeStart = line.IndexOf("TYPE=", StringComparison.Ordinal);
                if (typeStart <= 0)
                    continue;

                var typeEnd = line.IndexOf(':', typeStart);
                PhoneType = line.Substring(typeStart + 5, typeEnd - typeStart - 5);
            }
            else if (line.StartsWith("ORG:"))
                Organization = line[4..];
        }
    }

    public string GenerateVCard()
    {
        var vcard = "BEGIN:VCARD\r\n";
        vcard += "VERSION:3.0\r\n";

        if (!string.IsNullOrEmpty(FullName))
            vcard += $"FN:{FullName}\r\n";

        if (!string.IsNullOrEmpty(FamilyName) || !string.IsNullOrEmpty(GivenName))
            vcard += $"N:{FamilyName};{GivenName};;;\r\n";

        if (!string.IsNullOrEmpty(Organization))
            vcard += $"ORG:{Organization}\r\n";

        if (!string.IsNullOrEmpty(Phone))
        {
            var phoneTypeParam = !string.IsNullOrEmpty(PhoneType) ? $";TYPE={PhoneType}" : "";
            vcard += $"TEL{phoneTypeParam}:{Phone}\r\n";
        }

        vcard += "END:VCARD\r\n";
        return vcard;
    }
}
