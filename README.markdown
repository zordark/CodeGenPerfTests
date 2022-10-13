# CodeGenPerfTests
Code smaples for presentation "Code generation for performance optimization". The author is Igor Chevdar
### Regex
Examples of regex performance benchmarks: http://lh3lh3.users.sourceforge.net/reb.shtml.
### Xsd
The execution time of the benchmark method Scan should be subtracted from execution time other benchmarks methods (this is a time of reading data file)
### SwitchChars и SwitchStrings
The execution time of the benchmark method Empty should be subtracted from the execution time of other benchmarks methods (this is a time of shuffling source data to minimize branch predictor influence)
### Serialization
To get benchmark results of comparison ProtoBuf and ProtoBuf you should run benchmark "big_mixed"
### Running benchmarks
First of all, you should compile the solution with the release configuration. 
If you need to get benchmark results for Mono, you should install Mono x64 first and then run the compiled project under the .net 4.8 platform.