/*let druga = require('./druga');
let f = process.argv[2];
//console.log(f);
let c = [];
for(let i=0;i<f;i++)c[i]=process.argv[i+3];;
let izmesan = druga.mesaj(c);
for(let i=0;i<izmesan.length;i++)console.log(izmesan[i]);*/

const fs = require('fs'); // PROBLEM KAD SE OVDE STAVI SYNC
fs.readFile('./dokument.txt','utf-8',(err,data)=>{
    if(err){
        console.log(err);
    }
    else {
        console.log(data);
    }
})

const events = require('events');

const emitter = new events.EventEmitter();

emitter.addListener("ajmo",()=>{console.log("ajmo");});

emitter.emit("ajmo");

class Child extends events{
    constructor(childName)
    {
        super();
        this.childName = childName;
    }
}
let ko = new Child("dete");
ko.on("vici",function(){
    console.log(this.childName + "AAAAA");
})

ko.emit("vici");

const readLine = require('readline');

const rl = readLine.createInterface({ input:process.stdin,output:process.stdout});

const slucaj = Math.floor(Math.random()*5);

console.log("Koji je loto broj:");

rl.prompt();

rl.on('line',function(line){
    if(line == slucaj)
    {
        console.log("Bravo");
        rl.close();
    }
    else 
    {
        console.log("Ne");
        rl.prompt();
    }
})

//jos primer 2 i JSON u ovo ubaciti

let primer = require('./niz');
console.log(primer);

const pitanja = [ 'ime ', ' prezime ', ' mesto '];

let i = 0;

function ask()
{
    if(pitanja.length!=i)
    {
        console.log("Kako se "+pitanja[i]);
        rl.prompt();
        i++;
    }
    else 
    {
        console.log("Hvala na znanju!");
        rl.close();
    }
}

ask();

rl.on('line', function(line){
    fs.appendFile('baza.txt',line+'\n',function(err){
        if(err)throw err;
        ask();
        })});

rl.on('close',function(){
    fs.appendFileSync('baza.txt','-----\n');
});


//pipe

const rs = fs.createReadStream('izvor.mp4');

const ws = fs.createWriteStream('zaliv.mp4');


rs.pipe(ws);
let ip=1;
const ws2 = fs.createWriteStream("iseckan.mp4");

rs.on('data',function(deo){
    if(ip!=5 && ip!=6 && ip!=7)ws2.write(deo);
    ip++;
    //console.log(ip);
});
