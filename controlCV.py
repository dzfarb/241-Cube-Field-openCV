import socket
import cv2
import pandas

#set up socket and plugs into game stream intake
UDP_IP = "127.0.01"
UDP_PORT = 5065
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# WebCam Game Controller

# Assigning our static_back to None
static_back = None

frameIter = 0

location = []
# Initializing DataFrame, one column is start
# time and other column is end time
df = pandas.DataFrame(columns = ["Start", "End"])

# Capturing video
video = cv2.VideoCapture(0)

#key = cv2.waitKey(0)
#keeps recording video
while True:
	# Reading frame(image) from video
	check, frame = video.read()

	# Initializing motion = 0(no motion)
	motion = -1

	# Converting color image to gray_scale image
	gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

	# Converting gray scale image to GaussianBlur
	# so that change can be find easily
	gray = cv2.GaussianBlur(gray, (21, 21), 0)

	# In first iteration we assign the value
	# of static_back to our first frame
	if static_back is None:
		static_back = gray
		continue

	# Difference between static background
	# and current frame(which is GaussianBlur)
	diff_frame = cv2.absdiff(static_back, gray)

	# If change in between static background and
	# current frame is greater than 30 it will show white color(255)
	thresh_frame = cv2.threshold(diff_frame, 30, 255, cv2.THRESH_BINARY)[1]
	thresh_frame = cv2.dilate(thresh_frame, None, iterations = 2)
	# Finding contour of moving object
	cnts, _ = cv2.findContours(thresh_frame.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

	for contour in cnts:
		if cv2.contourArea(contour) < 10000:
			continue

		(x, y, w, h) = cv2.boundingRect(contour)
		# making green rectangle arround the moving object
		cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 3)

        #new stuff
		location.append(x)
		frameIter += 1
		#print(x)
		#break # if something is movingthen it append the end time of movement
		if frameIter > 0:
			if location[frameIter-1] < 15:
				sock.sendto(("center").encode(), (UDP_IP, UDP_PORT))
			elif location[frameIter-1] < 550:
				sock.sendto(("left").encode(), (UDP_IP, UDP_PORT))
			elif location[frameIter-1] > 650:
				sock.sendto(("right").encode(), (UDP_IP, UDP_PORT))
			else:
				sock.sendto(("center").encode(), (UDP_IP, UDP_PORT))

	# Displaying image in gray_scale
	#cv2.imshow("Gray Frame", gray)

	# Displaying the difference in currentframe to
	# the staticframe(very first_frame)
	#cv2.imshow("Difference Frame", diff_frame)


	cv2.imshow("Threshold Frame", thresh_frame)

	# Displaying color frame with contour of motion of object
	#cv2.imshow("Color Frame", frame)
	if cv2.waitKey(1) == ord("q"):
		break



video.release()

cv2.destroyAllWindows()
