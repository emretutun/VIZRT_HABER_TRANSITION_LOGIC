param(
  [string]$File,
  [string]$VizHost = "127.0.0.1",
  [int]$Port = 6100,
  [int]$TimeoutMs = 3000
)
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$Cmds = Get-Content $File | Where-Object { $_.Trim() -ne "" }
$client = New-Object System.Net.Sockets.TcpClient
$iar = $client.BeginConnect($VizHost, $Port, $null, $null)
if (-not $iar.AsyncWaitHandle.WaitOne($TimeoutMs)) { Write-Output "CONNECT TIMEOUT"; exit 1 }
$client.EndConnect($iar)
$stream = $client.GetStream()
$stream.ReadTimeout = $TimeoutMs
$enc = [System.Text.Encoding]::UTF8
$id = 1
foreach ($cmd in $Cmds) {
  $line = "$id $cmd" + [char]0
  $bytes = $enc.GetBytes($line)
  $stream.Write($bytes, 0, $bytes.Length)
  $stream.Flush()
  $sb = New-Object System.Text.StringBuilder
  $buf = New-Object byte[] 65536
  $done = $false
  try {
    while (-not $done) {
      $n = $stream.Read($buf, 0, $buf.Length)
      if ($n -le 0) { break }
      $chunk = $enc.GetString($buf, 0, $n)
      [void]$sb.Append($chunk)
      if ($chunk.Contains([string][char]0)) { $done = $true }
    }
  } catch { [void]$sb.Append(" <<READ TIMEOUT>>") }
  $resp = $sb.ToString().Replace([string][char]0, "")
  Write-Output ">> $cmd"
  Write-Output "<< $resp"
  Write-Output ""
  $id++
}
$client.Close()
