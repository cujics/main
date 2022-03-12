function mesaj(niz)
{
    let ktn=0;
    let korisni = [];
    let novo = [];
    for(let i=0;i<niz.length;i++)
    {
        let mesto = Math.round(Math.random()*(niz.length-1));
        //console.log("G:");console.log(korisni.indexOf(mesto));console.log(mesto);
        while(korisni.indexOf(mesto)!=-1)
        {
            if(ktn > 10) break;
            mesto = Math.round(Math.random()*(niz.length-1));
            //console.log(mesto);
            ktn++;
        }
        //console.log("IZASAO");
       // console.log(mesto);
        korisni.push(mesto);
        novo.push(niz[mesto]);
    }
    //for(let i=0;i<novo.length;i++)console.log(novo[i]);
    return novo;
}
module.exports.mesaj = mesaj;