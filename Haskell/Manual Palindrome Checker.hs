data Lista = K Int Lista | PK

nadji a
  | a==0 = 1
  | mod a 2==0 = a*nadji (a-1)
  | mod a 7==0 = a*nadji (a-1)
  | (a-1)>0 = nadji (a-1)
  | otherwise = 1

f a = nadji (a-1)

lista = [1 .. 50]
x = filter (\x -> (mod x 2 == 0 || mod x 7 == 0)) lista

instance Show Lista where
  show(K a PK) = show(a) ++ " "
  show(K a b) = show(a) ++ " " ++ show(b) ++ " "

y= K 5 ( K 6 ( K 7 ( K 1 PK)))

korak = 1
izvod :: Lista -> Int -> Int
izvod (K a PK) c = a*c;
izvod (K a b) c = a*c + izvod b c*10

pal :: Int -> Int -> Bool
pal a k 
  | a < 10 = True
  | (mod a 10) == (div a k) = pal (div (mod a k) 10) (div k 100)
  | otherwise = False

koef :: Int -> Int
koef a | a<10 = 1
koef a | a>9 = 10 * koef(div a 10)

ho m = pal m (koef m)