
//Task : https://cses.fi/problemset/task/2163
//Note : Some of these solutions have been done based on William Lin 150 problem set & other tutorials.

#include<bits/stdc++.h>
#include <ext/pb_ds/assoc_container.hpp>
#include <ext/pb_ds/tree_policy.hpp>
using namespace std;
using namespace __gnu_pbds;
template<class T> using oset =tree<T, null_type, less<T>, rb_tree_tag,tree_order_statistics_node_update> ;
oset<int> josep;
signed main(){
  int n,k=1,p;
  cin >> n >> k	;
  for(int i=1;i<=n;i++)
    josep.insert(i);
  while(josep.size()-1){
    p=(p+k)%(int)josep.size();
    cout << *(josep.find_by_order(p)) << " ";
    josep.erase(*(josep.find_by_order(p)));
    p%=(int)josep.size();
  }
  cout << *(josep.find_by_order(0));
}
