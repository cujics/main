derive :: (Fractional a) => a -> (a -> a) -> (a -> a)
derive h f x = (f (x+h) - f x) / h  

data Izraz = Broj Float | X 
  | Sab Izraz Izraz | Odu Izraz Izraz | Mno Izraz Izraz
  | Delj Izraz Izraz | Step Izraz Float
  deriving Eq

instance Show Izraz where
  show(Broj a)=show(a)
  show X = "x"
  show(Sab a b)="(" ++ show(a) ++ " + " ++ show(b) ++ ")"
  show(Odu a b)="(" ++ show(a) ++ " - " ++ show(b) ++ ")"
  show(Mno a b)="(" ++ show(a) ++ " * " ++ show(b) ++ ")"
  show(Delj a b)="(" ++ show(a) ++ " / " ++ show(b) ++ ")"
  show(Step a b)="(" ++ show(a) ++ " ^ " ++ show(b) ++ ")"

eval :: Izraz -> Float -> Float 
eval(Broj a) b =a
eval(X) b = b
eval(Sab a b) c = eval a c + eval b c
eval(Odu a b) c = eval a c - eval b c
eval(Mno a b) c = eval a c * eval b c
eval(Delj a b) c = eval a c / eval b c
eval(Step a b) c = (eval a c) ** (eval (Broj b) c)

s1 = Sab (Broj 5) X
s2 = Mno (Broj 6) X
s3 = Mno s1 s2
s4 = Step s3 2 
s5 = Step (Broj 2) 3

izvod :: Izraz -> Izraz
izvod X = Broj 1
izvod(Broj a)= Broj 0
izvod(Sab a b)=Sab (izvod a) (izvod b)
izvod(Odu a b)=Odu (izvod a) (izvod b)
izvod(Mno a b)=Sab (Mno (izvod a) b) (Mno (izvod b) a)
izvod(Delj a b)=Delj (Sab (Mno (izvod a) b) (Mno a (izvod b))) (Step b 2)
izvod(Step a b)=Mno (Broj b) (Step a (b-1))

f1 = Sab (Step X 2) (Broj 3)

simplify :: Izraz -> Izraz
simplify(Sab a (Broj 0)) = a
simplify(Odu a (Broj 0)) = a

--Ovako se poziva izvod : fi = izvod f1 
--Ovako se poziva pojednostavljenje : simplify fi