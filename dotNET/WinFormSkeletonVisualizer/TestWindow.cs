/************************************************************************************************************
 * 
 * Copyright (C) 2014-2017 Immotionar, a division of Beps Engineering.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
 * associated documentation files (the "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
 * LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 ************************************************************************************************************/

namespace IRoom.CsClient.Test
{
    using ImmotionAR.ImmotionRoom.Networking;
    using ImmotionAR.ImmotionRoom.TrackingServiceManagement;
    using ImmotionAR.ImmotionRoom.TrackingServiceManagement.DataStructures;
    using System;
    using System.Windows.Forms;
    using System.ComponentModel;
    using ImmotionAR.ImmotionRoom.Helpers;
    using ImmotionAR.ImmotionRoom.TrackingServiceManagement.DataSourcesManagement;
    using ImmotionAR.ImmotionRoom.Logger;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using ImmotionAR.ImmotionRoom.TrackingService.DataClient.Model;
    using System.Drawing;
    using ImmotionAR.ImmotionRoom.TrackingServiceManagement.Helpers;

    /// <summary>
    /// Main (and only) window of the program
    /// </summary>
    public partial class TestWindow : Form
    {
        #region Private fields

        /// <summary>
        /// TrackingServiceManager
        /// </summary>
        private TrackingServiceManagerBasic m_trackingService;

        /// <summary>
        /// Data read from the tracking service
        /// </summary>
        private SceneDataProvider m_sceneData;

        /// <summary>
        /// Last bodies arrived from the tracking service
        /// </summary>
        private List<TrackingServiceBodyData> m_lastBodies;

        /// <summary>
        /// True if the app has already seen its first bodies, false otherwise
        /// </summary>
        private bool m_firstBodies;

        /// <summary>
        /// Pen to draw the bodies with
        /// </summary>
        private Pen m_bodiesPen;

        /// <summary>
        /// Brush to draw the bodies with
        /// </summary>
        private Brush m_bodiesBrush;

        /// <summary>
        /// Translation to make the visualized skeletons more centered. First skeleton will be made with spine mid at the center of the window and
        /// all other skeletons will be translated accordingly
        /// </summary>
        private Point m_adjTranslation;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TestWindow()
        {
            //initialize window
            InitializeComponent();
            splitContainer1.IsSplitterFixed = true;
            tsLogTextBox.Text = "All ok";
            startButton.Enabled = false;
            stopButton.Enabled = false;

            //init  stuff
            m_lastBodies = null;
            m_firstBodies = false;
            m_bodiesPen = new Pen(Color.Black, 3);
            m_bodiesBrush = new SolidBrush(Color.LightSkyBlue);
            m_adjTranslation = new Point(0, 0);

            //create tracking service for .NET4.5
            CreateTrackingServiceForCurrentPlatform();

            //connect to its events
            m_trackingService.TrackingServiceStatusChanged += trackingService_TrackingServiceStatusChanged;

            //make the panel double-buffered, so we can draw it smoothly, then connect to its painting event, to draw skeletons
            SetDoubleBuffered(splitContainer1.Panel1);
            splitContainer1.Panel1.Paint += Panel1_Paint;
        }

        #endregion

        #region Forms methods overrides

        /// <summary>
        /// On form closing
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            //dispose everything and make all the cleanup (don't forget to Dispose all!)
            if (m_sceneData != null)
            {
                m_sceneData.NewSceneData -= sceneData_NewSceneData;
                m_sceneData.Dispose();
                m_sceneData = null;
            }

            m_trackingService.TrackingServiceStatusChanged -= trackingService_TrackingServiceStatusChanged;
            m_trackingService.Dispose();

            m_bodiesPen.Dispose();
            m_bodiesBrush.Dispose();
        }

        /// <summary>
        /// On drawing of the left panel of the split panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            //get the left panel (it will be already cleared by OnPaint method)
            System.Drawing.Graphics graphics = e.Graphics;

            //for each body
            if (m_lastBodies != null)
                foreach (var body in m_lastBodies)
                {
                    //we have to draw the joints. First of all we'll draw all joint lines, the limbs and then all the joint circles.
                    //This way the lines will not draw themselves above the skeleton joints (this is the way I want to draw them)
                    //This will require a double loop, but this is a sample, so doing something unoptimized is absolutely ok.
                    //Notice that we don't care about bodies Z order: first one, first served. If you want to draw the skeletons with
                    //the right depth order, you have to use other logic

                    //for each body joint
                    foreach (var joint in body.Joints)
                    {
                        //draw a line with its father, if any
                        TrackingServiceBodyJointTypes fatherType;

                        if (SkeletonHelpers.SkeletonJointsFathers.TryGetValue(joint.Key, out fatherType))
                        {
                            graphics.DrawLine(m_bodiesPen, GetKinectPointPositionInWindow(joint.Value.Position), GetKinectPointPositionInWindow(body.Joints[fatherType].Position));
                        }
                    }

                        //for each body joint
                        foreach (var joint in body.Joints)
                    {
                        Point jointPos = GetKinectPointPositionInWindow(joint.Value.Position);
                        //draw a circle to draw it                                                      
                        System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
                            jointPos.X - 25, jointPos.Y - 25, 50, 50);
                        graphics.DrawEllipse(m_bodiesPen, rectangle);
                        graphics.FillEllipse(m_bodiesBrush, rectangle);
                    }
                }
        }

        #endregion

        #region GUI Event management

        /// <summary>
        /// Exit menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Change Color menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //open a color dialog and get the color for the brush
            using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    m_bodiesBrush.Dispose(); //remember to do the cleanup!
                    m_bodiesBrush = new SolidBrush(cd.Color);
                }
            }
        }

        /// <summary>
        /// Initialize Tracking Service button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void initializeButton_Click(object sender, EventArgs e)
        {
            try
            {
                //initialize the tracking service manager, making it connect with underlying tracking service
                //(remember that this call is asynchronous)
                m_trackingService.InitializeAsync(false, true); //you can play with this line to try different initialization methods
                initializeButton.Enabled = false; //avoid multiple pressures of the button
            }
            catch (Exception ex)
            {
                tsLogTextBox.Text = ex.Message;
            }
        }

        /// <summary>
        /// Start Tracking button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                //request the tracking service to start tracking
                m_trackingService.RequestTrackingStart();
                startButton.Enabled = false; //avoid multiple pressures of the button
            }
            catch (Exception ex)
            {
                tsLogTextBox.Text = ex.Message;
            }
        }

        /// <summary>
        /// Trigger error button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void errorButton_Click(object sender, EventArgs e)
        {
            try
            {
                //request the tracking service to start tracking two times (this is not allowed, so Tracking Service will go in error state)
                m_trackingService.RequestTrackingStart();
                await Task.Delay(2900);
                m_trackingService.RequestTrackingStart();
                m_trackingService.RequestTrackingStop();
            }
            catch (Exception ex)
            {
                tsLogTextBox.Text = ex.Message;
            }
        }

        /// <summary>
        /// Stop Tracking button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                //request the tracking service to stop tracking
                m_trackingService.RequestTrackingStop();
                stopButton.Enabled = false; //avoid multiple pressures of the button
            }
            catch (Exception ex)
            {
                tsLogTextBox.Text = ex.Message;
            }
        }

        #endregion

        #region Tracking Service events management

        /// <summary>
        /// Called when tracking service changes its status
        /// </summary>
        /// <param name="eventArgs"></param>
        private void trackingService_TrackingServiceStatusChanged(TrackingServiceManagerStatusEventArgs eventArgs)
        {
            //write the new tracking service status on the labels
            this.Invoke(new Action(() => tsStatusLabel.Text = eventArgs.Status.ToString()));

            //if tracking service is returning to idle state
            if (eventArgs.Status == TrackingServiceManagerState.Idle)
            {
                //do cleanup (we have no more a stream of data, so kill  the provider)
                if (m_sceneData != null)
                {
                    m_sceneData.NewSceneData -= sceneData_NewSceneData;
                    m_sceneData.Dispose();
                    m_sceneData = null;
                }

                m_sceneData = null;
                m_lastBodies = null;
                m_firstBodies = false;
                m_adjTranslation = new Point(0, 0);

                //clear the panel with skeletons
                splitContainer1.Panel1.Invalidate();

                //re-enable start button
                this.Invoke(new Action(() => startButton.Enabled = true)); 
            }
            //else if it is going to tracking state
            else if(eventArgs.Status == TrackingServiceManagerState.Tracking)
            {
                //get a provider to read tracking service data
                m_sceneData = m_trackingService.StartSceneDataProvider();
                m_sceneData.NewSceneData += sceneData_NewSceneData;

                //re-enable stop button
                this.Invoke(new Action(() => stopButton.Enabled = true));
            }
            //else if it is going to error state
            else if (eventArgs.Status == TrackingServiceManagerState.Error)
            {
                //disable all buttons
                this.Invoke(new Action(() => stopButton.Enabled = startButton.Enabled = initializeButton.Enabled = false));

                //do cleanup (we have no more a stream of data, so kill  the provider)
                if (m_sceneData != null)
                    m_sceneData.Dispose();

                m_sceneData = null;
            }
        }

        /// <summary>
        /// Called when new data is received by the tracking service
        /// </summary>
        /// <param name="eventArgs"></param>
        private void sceneData_NewSceneData(SceneDataReadyEventArgs eventArgs)
        {            
            //get bodies and refresh window
            m_lastBodies = (List<TrackingServiceBodyData>)eventArgs.LastBodies;
            splitContainer1.Panel1.Invalidate();

            //if this is the first time we receive bodies, calculate the displacement necessary to see the body centered.
            //Notice that if the spine of the user is perceived at a height below the floor, we discard this frame since it contains glitch data
            if (!m_firstBodies)
            {
                m_firstBodies = (eventArgs.LastBodies != null && eventArgs.LastBodies.Count > 0 && eventArgs.LastBodies[0].Joints[TrackingServiceBodyJointTypes.SpineMid].Position.Y > 0);

                if (m_firstBodies)
                {
                    m_adjTranslation = Point.Empty;
                    Point spineMidPositionInWindow = GetKinectPointPositionInWindow(eventArgs.LastBodies[0].Joints[TrackingServiceBodyJointTypes.SpineMid].Position);
                    m_adjTranslation = new Point(-spineMidPositionInWindow.X + splitContainer1.Panel1.ClientSize.Width / 2,
                                                 -spineMidPositionInWindow.Y + splitContainer1.Panel1.ClientSize.Height / 2);
                }
            }
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Creates the tracking service and initializedImmotionRoom C# client package, which is Portable, for usage on a PC.        
        /// </summary>
        void CreateTrackingServiceForCurrentPlatform()
        {
            NetworkTools.PlatformHelpers = new HelpersNetworkTools(); //networking init
            m_trackingService = new TrackingServiceManagerBasic(new TcpClientFactory()); //tracking service creation
        }

        /// <summary>
        /// Make a control double buffered
        /// </summary>
        /// <remarks>
        /// Code from http://stackoverflow.com/questions/76993/how-to-double-buffer-net-controls-on-a-form
        /// </remarks>
        /// <param name="c">Control to make double buffered</param>
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }


        /// <summary>
        /// Get the position inside the Split Panel, corresponding to a particular 3D position.
        /// The method makes some kind of ortographic projection of the point + make the skeleton centered with initial conditions
        /// </summary>
        /// <param name="kinectPoint">Kinect point to get the coordinates of</param>
        /// <returns>Where to draw the kinect point</returns>
        Point GetKinectPointPositionInWindow(TrackingServiceVector3 kinectPoint)
        {
            //2D coordinates have Y towards down and are centered in top-left corner.
            //make the skeleton smallers and adjust to center them
            return new Point(m_adjTranslation.X + Width / 2 + (int)(kinectPoint.X * Width / 2.5f), m_adjTranslation.Y + Height - (int)(kinectPoint.Y * Height / 2.5f));
        }

        #endregion
    }
}
