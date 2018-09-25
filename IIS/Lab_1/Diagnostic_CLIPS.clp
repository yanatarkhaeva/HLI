
;;;======================================================
;;;   Automotive Expert System
;;;
;;;     This expert system diagnoses some simple
;;;     problems with your heart, commrad.
;;;
;;;     CLIPS Version 6.4 Example
;;;
;;;     To execute, merely load, reset and run.
;;;======================================================

;;****************
;;* DEFFUNCTIONS *
;;****************

(deffunction ask-question (?question $?allowed-values)
   (print ?question)
   (bind ?answer (read))
   (if (lexemep ?answer) 
       then (bind ?answer (lowcase ?answer)))
   (while (not (member$ ?answer ?allowed-values)) do
      (print ?question)
      (bind ?answer (read))
      (if (lexemep ?answer) 
          then (bind ?answer (lowcase ?answer))))
   ?answer)

(deffunction yes-or-no-p (?question)
   (bind ?response (ask-question ?question yes no y n))
   (if (or (eq ?response yes) (eq ?response y))
       then yes 
       else no))
       
(deffunction ch-s-s-lvl (?chss)
   (if (< ?chss 60)
      then low
      else (if (> ?chss 90)
            then hight
            else normal)))


;;;***************
;;;* QUERY RULES *
;;;***************


   
(defrule determine-shortness-of-breath ""
   (not (shortness-of-breath ?))
   (not (repair ?))
   =>
   (assert (shortness-of-breath (yes-or-no-p "У вас есть отдышка, comrade (yes/no)? "))))
   
(defrule determine-rhythm ""
   (not (rhythm ?))
   (not (repair ?))
   =>
   (assert (rhythm (yes-or-no-p "Сердечный ритм в норме, comrade (yes/no)? "))))   
   
(defrule determine-chss ""
   (not (chss ?))
   (not (repair ?))
   =>
   (printout t crlf "Какая у вас частота сердечного ритма, comrade (целое число)?")
   (bind ?chss (read))
   (assert (chss (ch-s-s-lvl ?chss))))
   
(defrule determine-chest-pains ""
   (not (chest-pain ?))
   (not (repair ?))
   =>
   (assert (chest-pain (yes-or-no-p "Вас беспокоят боли в области сердца, comrade (yes/no)? "))))

(defrule determine-ischemia ""
   (or (chest-pain yes)
        (shortness-of-breath yes))
   (not (repair ?))   
   =>
   (assert (ischemia yes)))
  
(defrule determine-pain-pre-infarct ""
   (ischemia yes)
   (not (repair ?))
   =>
   (assert (pain-pre-infarct (yes-or-no-p "Боль отдает в живот, горло, руку, лопатку  (yes/no)? "))))

(defrule determine-pain-sten ""
   (ischemia yes)
   (not (repair ?))
   =>
   (assert (pain-sten (yes-or-no-p "Боль отдает в левое плечо и внутреннюю поверхность левой руки, шею (yes/no)? "))))
   
(defrule determine-pain-sten ""
   (ischemia yes)
   (pain-pre-infarct no)
   (not (repair ?))   
   =>
   (assert (pain-sten yes))) 

(defrule determine-pain-stab-sten ""
   (pain-sten yes)
   (not (repair ?))
   =>
   (assert (pain-stab-sten
               (yes-or-no-p "Боль возникает только при физических нагрузках (yes/no)? "))))

(defrule determine-rhythm-disturbance ""
   (chest-pain no)
   (shortness-of-breath no)
   (rhythm no)
   (not (repair ?))
   =>
   (assert (rhythm-disturbance yes)))
   
(defrule determine-tah ""
   (rhythm yes)
   (chss hight)
   (not (repair ?))
   =>
   (assert (tah yes)))

(defrule determine-chss-up ""
   (tah yes)
   (not (repair ?))
   =>
   (assert (chss-up
              (yes-or-no-p "ЧСС резко поднимается до 140-240 ударов (yes/no)? "))))
              
(defrule determine-chss-down ""
   (tah yes)
   (not (repair ?))
   =>
   (assert (chss-down
              (yes-or-no-p "ЧСС резко приходит в норму (yes/no)? "))))

(defrule determine-irregularities-in-rhythm ""
   (rhythm-disturbance yes)
   (not (repair ?))
   =>
   (assert (irregularities-in-rhythm
              (yes-or-no-p "Ощущаются перебои в работе сердца (yes/no)? "))))
              
(defrule determine-physical-exercise ""
   (rhythm-disturbance yes)
   (not (repair ?))
   =>
   (assert (physical-exercise
              (yes-or-no-p "Физические нагрузки помогают избавиться от сбоев (yes/no)? "))))

;;;****************
;;;* REPAIR RULES *
;;;****************

(defrule stable-sten ""
   (pain-stab-sten yes)
   (not (repair ?))
   =>
   (assert (repair "Стабильная стенокардия")))

(defrule unstab-sten ""
   (pain-stab-sten no)
   (not (repair ?))
   =>
   (assert (repair "Нестабильная стенокардия"))) 

(defrule pre-inf ""
   (pain-pre-infarct yes)
   (not (repair ?))
   =>
   (assert (repair "Предынфарктное состояние")))     

(defrule sin-arithm ""
   (or (and (rhythm-disturbance yes)
            (irregularities-in-rhythm no))
            (and (rhythm-disturbance no)
            (physical-exercise no)
        ))
   (not (repair ?))
   =>
   (assert (repair "Синусоидная аритмия")))

(defrule extrasistol ""
   (or (and (rhythm-disturbance yes)
            (irregularities-in-rhythm yes))
            (and (rhythm-disturbance yes)
            (physical-exercise no)
        ))
   (not (repair ?))
   =>
   (assert (repair "Экстрасистолия")))

(defrule bradicard ""
   (rhythm yes)
   (chss low)
   (not (repair ?))
   =>
   (assert (repair "Брадикардия")))

(defrule sin-tah ""
   (or (and (tah yes)
            (chss-up no))
            (and (tah yes)
            (chss-down no)
        ))
   (not (repair ?))
   =>
   (assert (repair "Синусовая тахикардия")))

(defrule paroc-tah ""
   (or (and (tah yes)
            (chss-up yes))
            (and (tah yes)
            (chss-down yes)
        ))
   (not (repair ?))
   =>
   (assert (repair "Пароксизмальная тахикардия")))

(defrule youre-healthy ""
   (rhythm yes)
   (chest-pain no)
   (shortness-of-breath no)
   (chss normal)
   (not (repair ?))
   =>
   (assert (repair "Вы здоровы")))

;;;********************************
;;;* STARTUP AND CONCLUSION RULES *
;;;********************************

(defrule system-banner ""
  (declare (salience 10))
  =>
  (println crlf "The Engine Diagnosis Expert System" crlf))

(defrule print-repair ""
  (declare (salience 10))
  (repair ?item)
  =>
  (println crlf "Suggested Repair:" crlf)
  (println " " ?item crlf))
