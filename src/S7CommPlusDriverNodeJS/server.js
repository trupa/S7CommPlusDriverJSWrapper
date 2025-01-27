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
    methodName: 'CallFunction'
});

console.log("Connecting...");

var connected = 0;

Connect({ command: "createConnectionObject" }, (error, result) => {
    if (error) throw error;
    obj = result;
    console.log("Creating Object ...");
    console.log(result);    
});

Connect({ command: "initiateConnection", IPaddress: "192.168.18.25", password: "", timeout: 5000 }, (error, result) => {
    if (error) throw error;
    console.log("Initiating Connection ...");
    console.log(result);
});

Connect({ command: "getDataBlockInfoList" }, (error, result) => {
    if (error) throw error;
    console.log(result);
});

Connect({ command: "readVariable", tagSymbol: "DB_CharacterStrings.String_Hello_World" }, (error, result) => {
    if (error) throw error;
    console.log(result);
});

