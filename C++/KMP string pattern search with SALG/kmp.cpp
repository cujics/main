#include<iostream>
#include<chrono>
#include<random>
#include<cstring>
using namespace std;
const int mod=2e5;
const int almod=2e5+'A';
char s[mod];
char pat[mod];int mid=2e5;
int b[mod];
vector<int> indx,sindx;
vector<vector<char>> vek;
int sumaKMP,sumaSALG;
int rn(int mn,int mx)
{
	return mn + rand()%(mx-mn);
}
int rstr()
{
	for(int i=0;i<mod;i++)s[i]=rn(65,68);
	mid = 4;
	for(int i=0;i<mid;i++)pat[i]=rn(65,68);
}
bool fix(vector<char> ch)
{
	for(int i=0;i<mid;i++)if(ch[i]!=pat[i])return false;
	return true;
}
void kmppre(int n)
{
	int i=0;
	int j=-1;
	b[0]=j;
	while(i<n)
	{
		while(j>=0 && pat[i]!=pat[j])j=b[j];
		j++;i++;
		b[i]=j;
	}
}
void cekiraj(int& i)
{
	int g=0;
	for(int j=i;j<i+mid;g++,j++)if(pat[g]!=s[j]){i+=g-b[g];return;}
	indx.push_back(i);sumaKMP++;
	i+=mid-b[g];
	
}
void KMP(int n)
{
	int i=0;
	while(i+mid<n+1)
	{
		cekiraj(i);	
	}	
}
void sinisaALG(int n)
{
	int alter_sum=0;
	vector<char> priv;
	for(int i=0;i<mid;i++)alter_sum+=pat[i]+mod,alter_sum%=almod;
	int sum=0;
	for(int i=0;i<mid;i++)sum+=s[i]+mod,sum%=almod,priv.push_back((char)(s[i]));
	if(sum==alter_sum && fix(priv))sindx.push_back(0),vek.push_back(priv);
	for(int i=mid;i<n;i++){
		priv.erase(priv.begin());
		sum-=s[i-mid],sum+=s[i],sum%=almod;
		priv.push_back((char)(s[i]));
		if(sum==alter_sum && fix(priv))sindx.push_back(i-mid+1),vek.push_back(priv);
	}
}
void fct()
{
	long long number = 0;

    for( long long i = 0; i != 200000; ++i )
    {
       number += 5;
    }
}

int mainf()//za test drugog zadatka
{
	gets(s);
	gets(pat);
	cout << "patern:" << pat << "string:" << s << endl;
	kmppre(strlen(pat));mid = strlen(pat);
	KMP(strlen(s));cout << "B:";
	for(int i=0;i<strlen(s);i++)cout << b[i] << " ";cout << endl;
	for(int i:indx) cout << i << " "; cout << endl;
	sinisaALG(strlen(s));
	for(int i : sindx) cout << i << " ";cout << endl;

}
int main()//test brzine izmedju kmp i mog algoritma
{
	rstr();
	srand(time(0));
	auto t1 = chrono::high_resolution_clock::now();
	kmppre(strlen(pat));
	KMP(strlen(s));
	auto t2 = chrono::high_resolution_clock::now();
	auto duration = chrono::duration_cast<chrono::microseconds>(t2-t1).count();
	auto t3 = chrono::high_resolution_clock::now();
	sinisaALG(mod);
	auto t4 = chrono::high_resolution_clock::now();
	auto duration2 = chrono::duration_cast<chrono::microseconds>(t4-t3).count();
	cout << "vreme KMP algoritma:"<<duration << " mikrosekundi"<<endl<< "vreme sinisa algoritma:"<<duration2<<" mikrosekundi"<<endl;
	cout << "efikasnost KMP u odnosu na SALG:"<<(duration2*1.0)/duration << " puta brzi KMP"<<endl;
	cout << "rezultati KMp-a:"<<sumaKMP<<endl;
	cout << "rezultati sinisaALg-a:"<<vek.size()<<endl;
	char primer[6];
	for(int i=0;i<6;i++)primer[i]=s[i];
	cout << "deo random generisanog stringa:"<< primer<<"..." <<endl<< "rnd patern:" << pat<<endl;
}
