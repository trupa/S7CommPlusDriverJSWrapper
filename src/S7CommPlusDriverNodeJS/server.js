////'use strict';
////var http = require('http');
////var port = process.env.PORT || 1337;

////http.createServer(function (req, res) {
////    res.writeHead(200, { 'Content-Type': 'text/plain' });
////    res.end('Hello World\n');
////}).listen(port);

const edge = require('edge-js');

var Connect = edge.func({
    assemblyFile: '..\\S7CommPlusDllWrapper\\bin\\Debug\\S7CommPlusDllWrapper.dll', // path to .dll
    typeName: 'S7CommPlusDllWrapper.DllWrapper',
    methodName: 'Invoke'
});

console.log("Connecting...");

Connect({ command: "createConnectionObject" }, (error, result) => {
    if (error) throw error;
    console.log(result);
});

Connect({ command: "initiateConnection", IPaddress: "192.168.18.25", password: "", timeout: 5000 }, (error, result) => {
    if (error) throw error;
    console.log(result);
});

Connect({ command: "getDataBlockInfoList" }, (error, result) => {
    if (error) throw error;
    console.log(result);
});

Connect({ command: "readVariable", tagSymbol: "DB_FloatingPoint.Real_987d125"}, (error, result) => {
    if (error) throw error;
    console.log(result);
});

