Add-Type -AssemblyName System.IO.Compression.FileSystem
$zip = [System.IO.Compression.ZipFile]::OpenRead('D:\ProjectFile\ProjectReport\38JT\02Charger\protocol.docx')
$entry = $zip.GetEntry('word/document.xml')
$stream = $entry.Open()
$reader = New-Object System.IO.StreamReader($stream)
$xml = $reader.ReadToEnd()
$reader.Close()
$zip.Dispose()
[System.Xml.XmlDocument]$doc = New-Object System.Xml.XmlDocument
$doc.LoadXml($xml)
$ns = New-Object System.Xml.XmlNamespaceManager($doc.NameTable)
$ns.AddNamespace('w', 'http://schemas.openxmlformats.org/wordprocessingml/2006/main')
$paragraphs = $doc.SelectNodes('//w:p', $ns)
$text = ''
foreach($p in $paragraphs){
  $nodes = $p.SelectNodes('.//w:t', $ns)
  foreach($n in $nodes){
    $text += $n.InnerText
  }
  $text += "`r`n"
}
$text | Out-File -FilePath 'D:\ProjectFile\ProjectReport\38JT\02Charger\protocol_text.txt' -Encoding UTF8
Write-Host "Done extracting"
