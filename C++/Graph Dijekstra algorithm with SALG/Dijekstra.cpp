#include<iostream>
#include<vector>
#include<queue>
#include<stack>
using namespace std;

vector<vector<int>> listaSuseda{ {1, 2}, {3, 4}, {5}, {}, {6, 7}, {}, {}, {}, {} };
int brojCvorova = listaSuseda.size();
vector<bool> posecen(brojCvorova, false);
vector<vector<int>> dfs_drvo(brojCvorova);
vector<int> dolazni(brojCvorova);
vector<int> odlazni(brojCvorova);
vector<int> topolosko;
vector<vector<vector<int>>> listaSusedaDij{{{2,2},{3,5},{4,2}},{{3,3},{1,2},{5,1}},{{1,5},{2,3},{4,3},{6,1},{8,1},{5,1}},{{1,2},{3,3},{7,2}},{{2,1},{3,1},{9,7}},{{3,1},{7,2},{8,3}},{{4,2},{6,2}},{{3,1},{6,3},{9,1}},{}};
int brojcvorovaDij=listaSusedaDij.size();
vector<int> snaga(brojcvorovaDij,INT_MAX);
vector<bool> posecenDij(brojcvorovaDij,false);
int time = 0;

void sinisaDijDEBUG(int pocetni)//kalkulacija krece od 1
{
	posecenDij[pocetni-1]=true;
	for(auto p : listaSusedaDij[pocetni-1])
	{
		if(snaga[pocetni-1]+p[1]<snaga[p[0]-1])
		{
			cout << "snaga["<<pocetni-1<<"]="<<snaga[pocetni-1]<<"+p[1]="<<p[1]<<" je manje od snaga["<<p[0]-1<<"]="<<snaga[p[0]-1]<<endl;
			snaga[p[0]-1]=snaga[pocetni-1]+p[1];
			cout << "Ubacen cvor:"<<p[0] << endl;
			sinisaDijDEBUG(p[0]);
		}
	}
}

//DOMACI DIJEKSTROV ALG

void sinisaDij(int pocetni)//kalkulacija krece od 1
{
	posecenDij[pocetni-1]=true;
	for(auto p : listaSusedaDij[pocetni-1])if(snaga[pocetni-1]+p[1]<snaga[p[0]-1])snaga[p[0]-1]=snaga[pocetni-1]+p[1],sinisaDij(p[0]);
		
}

//KRAJ DOMACEG

void dfs(int cvor) {
	posecen[cvor] = true;
	time++;
	dolazni[cvor] = time;
	// rekurzivno prolazimo kroz sve njegove susede
	// koje ranije nismo obisli
	for (auto sused : listaSuseda[cvor]) {
		if (!posecen[sused]) {
			// u DFS drvo dodajemo granu iz tekuceg ka novom cvoru
			dfs_drvo[cvor].push_back(sused);
			dfs(sused);
		}
	}
	time++;
	odlazni[cvor] = time;
	topolosko.push_back(cvor);
}

//DOMACI TEST DIJEKSTROVOG ALG ZA BILO KOJE POLAZNE VREDNOSTI

int main()
{
	int pol,zav;
	cout << "Unesi polazni cvor(od 1 do 9):";
	cin >> pol;
	cout << "Unesi krajnji cvor(od 1 do 9):";
	cin >> zav;
	snaga[pol-1]=0;
	sinisaDij(pol);
	cout << "Najkraca udaljenost po Dijekstrinom algoritmu je od cvora "<<pol << " do cvora "<<zav << " je:"<<snaga[zav-1];
}

//KRAJ DOMACI


// funkcija koja vrsi DFS obilazak datog grafa iz datog cvora

int main2() {
	int cvor = 0;
	dfs(cvor);
	cout << brojCvorova << " " << topolosko.size() << endl;
	if (topolosko.size() == brojCvorova)cout << "Povezan graf"<<endl;
	else cout << "Nepovezan graf" << endl;


	cout << "Grane DFS drveta su: " << endl;
	for (int i = 0; i < dfs_drvo.size(); i++)
		for (int j = 0; j < dfs_drvo[i].size(); j++)
			cout << "(" << i << "," << dfs_drvo[i][j] << ")" << endl;
	cout << endl << "Dolazno i odlazno vreme: " << endl;
	for (int cvor = 0; cvor < brojCvorova; cvor++)
		cout << "Čvor: " << cvor << "   " << dolazni[cvor] << "/" << odlazni[cvor] << endl;

	cout << endl << "Topološki sortirani čvorovi:" << endl;
	for (int cvor = topolosko.size() - 1; cvor >= 0; cvor--)
		cout << "Čvor: " << topolosko[cvor] << "  ";
	cout << endl;
	return 0;
}
