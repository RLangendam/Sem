# Sem
Sem for Windows

Example usage:
`sem 1 timeout 3`

This will execute `cmd.exe /c timeout 3` within the scope of a global semaphore named "timeout" with a maximum count of 1.
Multiple of such calls in parallel will execute `timeout` sequentially.

See also,
* https://www.gnu.org/software/parallel/sem.html
* https://docs.microsoft.com/en-us/dotnet/api/system.threading.semaphore?view=netframework-4.7.2
