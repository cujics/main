
//Task : https://cses.fi/problemset/task/1090
//Note : Some of these solutions have been done based on William Lin 150 problem set & other tutorials.

#include<bits/stdc++.h>
#define nmax (int)1e5*2
using namespace std;
int ar[nmax],x,n,ans;
int main()
{
	scanf("%d%d",&n,&x);
	for(int i=0;i<n;i++)scanf("%d",&ar[i]);
	sort(ar,ar+n);
	for(int i=0,j=n-1;i<j;)
	{
		while(i<j&&ar[i]+ar[j]>x)j--;
		if(i>=j)break;
		ans++;
		j--;i++;
	}
	printf("%d",n-ans);
}
