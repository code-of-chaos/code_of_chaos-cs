// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Sample;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class EveningClassData {
    [CsvColumn("typeURI")] public string TypeURI {get;set;} = string.Empty;
    [CsvColumn("assetId.identificator")] public string AssetIdIdentificator {get;set;} = string.Empty;
    [CsvColumn("assetId.toegekendDoor")] public string AssetIdToegekendDoor {get;set;} = string.Empty;
    [CsvColumn("besturingsconnector")] public string Besturingsconnector {get;set;} = string.Empty;
    [CsvColumn("bron.typeURI")] public string BronTypeURI {get;set;} = string.Empty;
    [CsvColumn("bronAssetId.identificator")] public string BronAssetIdIdentificator {get;set;} = string.Empty;
    [CsvColumn("bronAssetId.toegekendDoor")] public string BronAssetIdToegekendDoor {get;set;} = string.Empty;
    [CsvColumn("datumOprichtingObject")] public string DatumOprichtingObject {get;set;} = string.Empty;
    [CsvColumn("doel.typeURI")] public string DoelTypeURI {get;set;} = string.Empty;
    [CsvColumn("doelAssetId.identificator")] public string DoelAssetIdIdentificator {get;set;} = string.Empty;
    [CsvColumn("doelAssetId.toegekendDoor")] public string DoelAssetIdToegekendDoor {get;set;} = string.Empty;
    [CsvColumn("heeftAansluitkastGeintegreerd")] public string HeeftAansluitkastGeintegreerd {get;set;} = string.Empty;
    [CsvColumn("heeftAntiVandalisme")] public string HeeftAntiVandalisme {get;set;} = string.Empty;
    [CsvColumn("isActief")] public string IsActief {get;set;} = string.Empty;
    [CsvColumn("isFaunavriendelijk")] public string IsFaunavriendelijk {get;set;} = string.Empty;
    [CsvColumn("kleurArmatuur")] public string KleurArmatuur {get;set;} = string.Empty;
    [CsvColumn("kleurTemperatuur")] public string KleurTemperatuur {get;set;} = string.Empty;
    [CsvColumn("lichtkleur")] public string Lichtkleur {get;set;} = string.Empty;
    [CsvColumn("lichtpuntHoogte")] public string LichtpuntHoogte {get;set;} = string.Empty;
    [CsvColumn("lumenOutput")] public string LumenOutput {get;set;} = string.Empty;
    [CsvColumn("merk")] public string Merk {get;set;} = string.Empty;
    [CsvColumn("modelnaam")] public string Modelnaam {get;set;} = string.Empty;
    [CsvColumn("naam")] public string Naam {get;set;} = string.Empty;
    [CsvColumn("notitie")] public string Notitie {get;set;} = string.Empty;
    [CsvColumn("protector")] public string Protector {get;set;} = string.Empty;
    [CsvColumn("systeemvermogen")] public string Systeemvermogen {get;set;} = string.Empty;
    [CsvColumn("theoretischeLevensduur")] public string TheoretischeLevensduur {get;set;} = string.Empty;
    [CsvColumn("toestand")] public string Toestand {get;set;} = string.Empty;
    [CsvColumn("geometry")] public string Geometry {get;set;} = string.Empty;
}
