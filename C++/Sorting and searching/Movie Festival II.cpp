
//Task : https://cses.fi/problemset/task/1632
//Note : Some of these solutions have been done based on William Lin 150 problem set & other tutorials.

#include<iostream>
#include<set>
#include<algorithm>
#include<vector>
using namespace std;
 
int main()
{
	int n,k;cin >> n >> k;
	vector<pair<int,int>> filmovi(n);
	for(int i=0;i<n;i++)cin >> filmovi[i].second >> filmovi[i].first;
	sort(begin(filmovi),end(filmovi));
	
	int resenje=0;
	multiset<int> slobodno;
	for(int i=0;i<k;i++)slobodno.insert(0);
	
	for(int i=0;i<n;i++)
	{
		//for(int j:slobodno)cout << "J:"<<j << " ";
		//cout << endl;
		auto it=slobodno.upper_bound(filmovi[i].second);
		if(it == begin(slobodno))continue;
		slobodno.erase(--it);
		slobodno.insert(filmovi[i].first);
		resenje++;
	}
	cout << resenje;
}
