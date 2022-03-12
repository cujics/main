
//Task : https://cses.fi/problemset/task/1745
//Note : Some of these solutions have been done based on William Lin 150 problem set & other tutorials.

#include<iostream>
#include<vector>
using namespace std;
bool dp[100001];
vector<int> v;
int n,ar[100],prx;
int main()
{
	cin >> n;
	for(int i=0;i<n;i++)cin >> ar[i],prx+=ar[i];dp[0]=1;
	for(int j=0;j<n;j++)for(int i=prx;i>=ar[j];i--)dp[i]|=dp[i-ar[j]];
	for(int i=1;i<=prx;i++)if(dp[i])v.push_back(i);
	cout << v.size() << "\n";
	for(int i:v)cout << i << " ";
}
